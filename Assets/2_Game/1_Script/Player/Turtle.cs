using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : PlayerMng
{
    public GameObject BulletPre;

    public Transform JoyStickTr;
    public Transform GunParentTr;
    public Transform GunTr;

    public PlayerMapPosition GetMapPlayer;

    Vector2 PlayerMoveVec;

    const float correction = 90f * Mathf.Deg2Rad;   // �����
    //public float fBulletDirect;
    float fRotateDegree;
    float fBulletDelay;
    float fGunRot;

    // Start is called before the first frame update
    void Start()
    {
        SGameMng.I.PlayerType = PLAYERTYPE.TURTLE;
        _PlayerWeaponType = WEAPONTYPE.RANGED_WEAPON;
        //direction = PLAYERDIRECT.DONTMOVE;
        _PlayerSr = GetComponent<SpriteRenderer>();
        _PlayerRig = GetComponent<Rigidbody2D>();
        _fMoveSpeed = 5.0f;
        _nPlayerHp = 10;
        _nFullHp = 10;
        _bMoveAccess = true;
        _bDmgAccess = true;
        WeaponSetting(_PlayerWeaponType);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            WeaponSetting(_PlayerWeaponType);
        }
        PlayerState();
        if (!SGameMng.I.bMobileOn)
        {
            Move();
            PlayerSkill();
        }
        else
        {
            getKey();
            movement();
            rotation();
            PlayerSkill();

            if (_bAttackAccess)
            {
                Attack();
            }
        }
        if (Time.time > fBulletDelay + _fAttackSpeed)                                                                                // 0.1f�κ��� ����ȭ�ؼ� �Ѿ� ������ ����
        {
            _bBulletShooting = false;
        }
    }


    void PlayerState()
    {
        if (_nPlayerHp > 0)
        {
            if (!SGameMng.I.bMobileOn)
                WeaponRot();
        }
        else
        {
            _bPlayerDie = true;
            Debug.Log("�÷��̾� ���");
        }

        if (_bPlayerDie)
        {
            _bMoveAccess = false;
            _bDmgAccess = false;
        }
    }

    void rotation()
    {
        if (_MoveVec.Equals(Vector3.zero))
            return;

        fGunRot = (Mathf.Atan2(_MoveVec.y, _MoveVec.x) - correction) * Mathf.Rad2Deg;
        if (_bMoveAccess)
            GunParentTr.rotation = Quaternion.Euler(0f, 0f, fGunRot - 90f);
        if (JoyStickTr.localPosition.x < 0)
        {
            _PlayerSr.flipX = false;
        }
        else if (JoyStickTr.localPosition.x > 0)
        {
            _PlayerSr.flipX = true;
        }
    }

    void Move()
    {
        if (_bMoveAccess)
        {
            PlayerMoveVec = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                //direction = PLAYERDIRECT.UP;
                PlayerMoveVec.y += _fMoveSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                //direction = PLAYERDIRECT.DOWN;
                PlayerMoveVec.y -= _fMoveSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                //direction = PLAYERDIRECT.LEFT;
                _PlayerSr.flipX = false;
                PlayerMoveVec.x -= _fMoveSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                //direction = PLAYERDIRECT.RIGHT;
                _PlayerSr.flipX = true;
                PlayerMoveVec.x += _fMoveSpeed;
            }
            _PlayerRig.velocity = PlayerMoveVec;
        }
        if (Input.GetMouseButton(0) && !_bPlayerDie)
        {
            Attack();
        }
    }

    void WeaponRot()
    {
        Vector3 mPosition = Input.mousePosition;
        Vector3 oPosition = transform.position;

        mPosition.z = oPosition.z - Camera.main.transform.position.z;

        Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);

        float dy = target.y - oPosition.y;
        float dx = target.x - oPosition.x;
        fRotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        GunParentTr.rotation = Quaternion.Euler(0f, 0f, fRotateDegree - 180f);                                     // �� ��ġ ȸ��(���콺 ����) ���Ŀ� ���� ���� �������� ��ȯ

        //if (!bBulletDirect)
        //{
        //    transform.rotation = Quaternion.Euler(0f, 0f, _fRotateDegree);
        //}
    }
    void PlayerSkill()
    {
        if (_bSkillOn)
        {
            SGameMng.I.MonsterMngSc.bAreaSkillOn = true;
            _bSkillOn = false;
        }
    }

    void Attack()
    {
        if (!_bBulletShooting)
        {
            fBulletDelay = Time.time;

            if (!SGameMng.I.bMobileOn)
            {
                if (_nBulletAmount > 0)
                {
                    if (!_bBulletReloading)
                    {
                        Instantiate(BulletPre, GunTr.position, Quaternion.Euler(0f, 0f, fRotateDegree - 90f));
                        _nBulletAmount--;
                    }
                }
                if (_nBulletAmount <= 0 && !_bBulletReloading)
                {
                    StartCoroutine(WeaponReload());
                }
            }
            else
            {
                if (_nBulletAmount > 0)
                {
                    if (!_bBulletReloading)
                    {
                        Instantiate(BulletPre, GunTr.position, Quaternion.Euler(0f, 0f, fGunRot));
                        _nBulletAmount--;
                    }
                }
                if (_nBulletAmount <= 0 && !_bBulletReloading)
                {
                    StartCoroutine(WeaponReload());
                }
            }
            _bBulletShooting = true;
        }
    }

    IEnumerator WeaponReload()
    {
        _bBulletReloading = true;
        yield return new WaitForSeconds(_fReloadTime);
        WeaponSetting(_PlayerWeaponType);
        _bBulletReloading = false;
    }



    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.transform.CompareTag("Monster"))
        {
            if (_bDmgAccess)
            {
                StartCoroutine(_DamageCtrl());
                _nPlayerHp -= 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //���Ǻο��� col.tag�� äŷ�Ѵ�
        //�̴ϸ� ĳ���� �̵�
        GetMapPlayer.GetMapPlayerMove(col);
        //�÷��̾� ĳ���� �̵�
        transform.Translate(GetMapPlayer.GetPlayerMove(col));
        //�� ���İ� ����
        StartCoroutine(MoveMapAlphaCtrl(col));

    }
}
