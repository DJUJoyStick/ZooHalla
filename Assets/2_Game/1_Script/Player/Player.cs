using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerMng
{
    public GameObject BulletPre;

    public Transform JoyStickTr;
    public Transform GunParentTr;
    public Transform GunTr;

    public PlayerMapPosition GetMapPlayer;

    Coroutine RatPassiveCor;

    Vector2 PlayerMoveVec;

    int nSaveHealth;

    const float correction = 90f * Mathf.Deg2Rad;   // 모바일
    float fRotateDegree;
    float fBulletDelay;
    float fGunRot;

    bool bRatPassive = false;

    void Start()
    {
        SGameMng.I.PlayerType = PLAYERTYPE.RAT;
        if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
        {
            PlayerInit(WEAPONTYPE.RANGED_WEAPON, 10.0f, 5, 5, true, true);
            RatPassiveCor = StartCoroutine(AutoHealth());
        }
        else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
        {
            PlayerInit(WEAPONTYPE.RANGED_WEAPON, 5.0f, 10, 10, true, true);
        }
        else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
        {

        }
        _PlayerRig = GetComponent<Rigidbody2D>();
        _PlayerSr = GetComponent<SpriteRenderer>();
        _PlayerAnime = GetComponent<Animator>();
    }

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
        if (Time.time > fBulletDelay + _fAttackSpeed)
        {
            _bBulletShooting = false;
        }
        if (SGameMng.I.bJoystickDown && !_bPlayerDie)
        {
            if (JoyStickTr.localPosition.x < 0)
            {
                _PlayerSr.flipX = false;
                _PlayerWalkAnime(true, false);
            }
            else if (JoyStickTr.localPosition.x > 0)
            {
                _PlayerSr.flipX = false;
                _PlayerWalkAnime(false, true);
            }
        }
        else
            _PlayerWalkAnime(false, false);

        if (_nPlayerHp <= 0)
        {
            if (JoyStickTr.localPosition.x > 0)
                _PlayerSr.flipX = true;
            _PlayerAnime.SetBool("isDying", true);
        }
    }
    void PlayerInit(WEAPONTYPE w_type, float movespeed, int hp, int fullhp, bool moveaccess, bool dmgacces)
    {
        _PlayerWeaponType = w_type;
        _fMoveSpeed = movespeed;
        _nPlayerHp = hp;
        _nFullHp = fullhp;
        _bMoveAccess = moveaccess;
        _bDmgAccess = dmgacces;
        WeaponSetting(_PlayerWeaponType);
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
            _PlayerSr.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            GunParentTr.gameObject.SetActive(false);
            if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
                StopCoroutine(RatPassiveCor);
        }

        if (_bPlayerDie)
        {
            _bMoveAccess = false;
            _bDmgAccess = false;
        }

        if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
        {
            if (!bRatPassive && _nPlayerHp <= 3)
            {
                RatPassiveCor = StartCoroutine(AutoHealth());
                bRatPassive = true;
            }
        }
    }

    void rotation()
    {
        if (_MoveVec.Equals(Vector3.zero))
            return;

        fGunRot = (Mathf.Atan2(_MoveVec.y, _MoveVec.x) - correction) * Mathf.Rad2Deg;
        if (_bMoveAccess)
            GunParentTr.rotation = Quaternion.Euler(0f, 0f, fGunRot - 90f);
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

        GunParentTr.rotation = Quaternion.Euler(0f, 0f, fRotateDegree - 180f);                                     // 총 위치 회전(마우스 방향) 추후에 고정 몬스터 방향으로 변환

        //if (!bBulletDirect)
        //{
        //    transform.rotation = Quaternion.Euler(0f, 0f, _fRotateDegree);
        //}
    }
    void PlayerSkill()
    {
        if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
        {
            if (_bSkillOn)
            {
                SGameMng.I.MonsterMngSc.bAreaSkillOn = true;
                _bSkillOn = false;
            }
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
                if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
                {
                    int nRand = Random.Range(1, 100);
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
                else
                {
                    _nPlayerHp -= 1;
                    nSaveHealth = _nPlayerHp;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //정의부에서 col.tag를 채킹한다
        //미니맵 캐릭터 이동
        GetMapPlayer.GetMapPlayerMove(col);
        //플레이어 캐릭터 이동
        transform.Translate(GetMapPlayer.GetPlayerMove(col));
        //맵 알파값 조정
        StartCoroutine(MoveMapAlphaCtrl(col));

    }
}
