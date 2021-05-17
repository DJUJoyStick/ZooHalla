using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : PlayerMng
{
    public GameObject BulletPre;

    public Transform JoyStickTr;
    public Transform GunParentTr;
    public Transform GunTr;

    public PlayerMapPosition GetMapPlayer;

    SpriteRenderer PlayerSr;

    Rigidbody2D PlayerRig;

    Coroutine RatPassiveCor;

    Vector2 PlayerMoveVec;
    Vector3 SaveMoveVec;
    public Vector3 MoveVec;                        // �����
    Vector3 RotVec;                                // �����

    public PLAYERTYPE Playertype;

    int nSaveHealth;

    const float correction = 90f * Mathf.Deg2Rad;   // �����
    //public float fBulletDirect;
    float fRotateDegree;
    float fBulletDelay;
    float fGunRot;

    bool bRatPassive = false;

    // Start is called before the first frame update
    void Start()
    {
        Playertype = PLAYERTYPE.RAT;
        //direction = PLAYERDIRECT.DONTMOVE;
        PlayerSr = GetComponent<SpriteRenderer>();
        PlayerRig = GetComponent<Rigidbody2D>();
        _fMoveSpeed = 10.0f;
        _nPlayerHp = 5;
        _nEvasion = 30;
        _bMoveAccess = true;
        _bDmgAccess = true;
        ChangeGunType();
        SGameMng.I.nFullHp = 5;
        RatPassiveCor = StartCoroutine(AutoHealth());
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Time.time > fBulletDelay + 0.1f)                                                                                // 0.1f�κ��� ����ȭ�ؼ� �Ѿ� ������ ����
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

        if (!bRatPassive && _nPlayerHp <= 3)
        {
            RatPassiveCor = StartCoroutine(AutoHealth());
            bRatPassive = true;
        }
    }

    void getKey()
    {
        MoveVec = new Vector3(CnControls.CnInputManager.GetAxis("Horizontal"), CnControls.CnInputManager.GetAxis("Vertical"));
        RotVec = new Vector3(CnControls.CnInputManager.GetAxis("RotateX"), CnControls.CnInputManager.GetAxis("RotateY"));
    }

    void movement()
    {
        if (_bMoveAccess)
            PlayerRig.velocity = MoveVec * _fMoveSpeed;
    }

    void rotation()
    {
        if (MoveVec.Equals(Vector3.zero))
            return;

        fGunRot = (Mathf.Atan2(MoveVec.y, MoveVec.x) - correction) * Mathf.Rad2Deg;
        if (_bMoveAccess)
            GunParentTr.rotation = Quaternion.Euler(0f, 0f, fGunRot - 90f);
        if (JoyStickTr.localPosition.x < 0)
        {
            PlayerSr.flipX = false;
        }
        else if (JoyStickTr.localPosition.x > 0)
        {
            PlayerSr.flipX = true;
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
                PlayerSr.flipX = false;
                PlayerMoveVec.x -= _fMoveSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                //direction = PLAYERDIRECT.RIGHT;
                PlayerSr.flipX = true;
                PlayerMoveVec.x += _fMoveSpeed;
            }
            PlayerRig.velocity = PlayerMoveVec;
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
                    StartCoroutine(BulletReload());
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
                    StartCoroutine(BulletReload());
                }
            }
            _bBulletShooting = true;
        }
    }

    IEnumerator AutoHealth()
    {
        yield return new WaitForSeconds(5.0f);
        if (_nPlayerHp.Equals(nSaveHealth))
        {
            if (_nPlayerHp <= 3)
            {
                _nPlayerHp++;
                nSaveHealth++;
            }
        }
        bRatPassive = false;
    }

    IEnumerator BulletReload()
    {
        _bBulletReloading = true;
        yield return new WaitForSeconds(_fReloadTime);
        ChangeGunType();
        _bBulletReloading = false;
    }

    IEnumerator DamageCtrl()
    {
        PlayerSr.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 125 / 255f);                 // �ǰݽ� �÷��̾��� ��������Ʈ ���İ� ����(��� �����̶�� �ǹ� *�ӽ���)
        _bDmgAccess = false;
        yield return new WaitForSeconds(1.5f);
        PlayerSr.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        _bDmgAccess = true;
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.transform.CompareTag("Monster"))
        {
            int nRand = Random.Range(1, 100);
            if (_bDmgAccess)
            {
                StartCoroutine(DamageCtrl());
                if (nRand > 30)
                {
                    _nPlayerHp -= 1;
                    nSaveHealth = _nPlayerHp;

                    StopCoroutine(RatPassiveCor);
                    if (bRatPassive)
                    {
                        bRatPassive = false;
                    }
                }
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
        StartCoroutine(MoveMapAlphaCtrl());
    }
}