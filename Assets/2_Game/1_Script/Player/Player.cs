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

    public UnityEngine.UI.Image turtleskillimg;

    Vector2 PlayerMoveVec;

    int nSaveHealth;

    const float correction = 90f * Mathf.Deg2Rad;
    float fRotateDegree;
    float fBulletDelay;
    float fGunRot;

    bool bRatPassive = false;

    void Start()
    {
        SGameMng.I.PlayerType = PLAYERTYPE.WOLF;
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
            PlayerInit(WEAPONTYPE.RANGED_WEAPON, 7.0f, 7, 7, true, true);
        }
        _PlayerRig = GetComponent<Rigidbody2D>();
        _PlayerSr = GetComponent<SpriteRenderer>();
        _PlayerAnime = GetComponent<Animator>();
        JoyStickTr = GameObject.Find("Stick").transform;
        GetMapPlayer = GameObject.Find("LevelGenerator").GetComponent<PlayerMapPosition>();
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
                if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
                {
                    if (!_bSkillOn)
                        _bLookRight = false;
                }
                else
                    _bLookRight = false;
                _PlayerSr.flipX = false;
                _PlayerWalkAnime(true, false);
            }
            else if (JoyStickTr.localPosition.x > 0)
            {
                if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
                {
                    if (!_bSkillOn)
                        _bLookRight = true;
                }
                else
                    _bLookRight = true;
                _PlayerSr.flipX = false;
                _PlayerWalkAnime(false, true);
            }
        }
        else
            _PlayerWalkAnime(false, false);

        if (_nPlayerHp <= 0)
        {
            if (JoyStickTr.localPosition.x > 0 && !_PlayerAnime.GetBool("isDying"))
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
        else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
        {
            TurtleSkill();
        }
        else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
        {
            DeathHard();
            WolfSkill();
        }
    }

    void rotation()
    {
        if (_MoveVec.Equals(Vector3.zero))
            return;

        fGunRot = (Mathf.Atan2(_MoveVec.y, _MoveVec.x) - correction) * Mathf.Rad2Deg;
        if (_bMoveAccess)
            GunParentTr.rotation = Quaternion.Euler(0f, 0f, fGunRot - 90f);

        if (!SGameMng.I.TargetEnemyTr.Equals(null))
        {
            if (SGameMng.I.TargetEnemySc.bFindMobOn)
            {
                float guny = SGameMng.I.TargetEnemyTr.position.y - transform.position.y;
                float gunx = SGameMng.I.TargetEnemyTr.position.x - transform.position.x;
                float fBulletDegree = Mathf.Atan2(guny, gunx) * Mathf.Rad2Deg;

                GunParentTr.rotation = Quaternion.AngleAxis(fBulletDegree - 180f, Vector3.forward);
            }
        }
    }

    void Move()
    {
        if (_bMoveAccess)
        {
            PlayerMoveVec = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
                PlayerMoveVec.y += _fMoveSpeed;
            if (Input.GetKey(KeyCode.S))
                PlayerMoveVec.y -= _fMoveSpeed;
            if (Input.GetKey(KeyCode.A))
            {
                _PlayerSr.flipX = false;
                PlayerMoveVec.x -= _fMoveSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
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

        if (!SGameMng.I.TargetEnemyTr.Equals(null))
        {
            if (SGameMng.I.TargetEnemySc.bFindMobOn)
            {
                float guny = SGameMng.I.TargetEnemyTr.position.y - transform.position.y;
                float gunx = SGameMng.I.TargetEnemyTr.position.x - transform.position.x;
                float fBulletDegree = Mathf.Atan2(guny, gunx) * Mathf.Rad2Deg;

                GunParentTr.rotation = Quaternion.AngleAxis(fBulletDegree - 180f, Vector3.forward);
            }
        }
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
        else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
        {
            if (_bSkillOn)
            {
                _bMoveAccess = false;
            }
            else
            {
                _bMoveAccess = true;
            }
        }
        else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
        {

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

    void TurtleSkill()              //거북이 액티브
    {

        if (!_bSkillOn)
        {
            if (turtleskillimg.fillAmount < 1)
                turtleskillimg.fillAmount = turtleskillimg.fillAmount + 0.001f;
        }
        else
        {
            if (turtleskillimg.fillAmount > 0)
                turtleskillimg.fillAmount = turtleskillimg.fillAmount - 0.005f;
        }
        if (turtleskillimg.fillAmount <= 0)
        {
            if (_bLookRight)
            {
                _PlayerAnime.SetBool("isRSkill", false);
                SGameMng.I.PlayerSc._bSkillOn = false;
                SGameMng.I.PlayerSc._bDmgAccess = true;
            }
            else
            {
                _PlayerAnime.SetBool("isLSkill", false);
                SGameMng.I.PlayerSc._bSkillOn = false;
                SGameMng.I.PlayerSc._bDmgAccess = true;
            }
        }
    }

    void WolfSkill()                // 늑대 스킬
    {
        if (_bSkillOn)
        {
            StartCoroutine(ICanStopMe());
            _bSkillOn = false;
        }
    }

    IEnumerator WeaponReload()
    {
        _bBulletReloading = true;
        yield return new WaitForSeconds(_fReloadTime);
        WeaponSetting(_PlayerWeaponType);
        _bBulletReloading = false;
    }

    IEnumerator AutoHealth()        // 생쥐 패시브
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

    IEnumerator ICanStopMe()
    {
        float fSaveAttackSpeed = _fAttackSpeed;
        _fAttackSpeed = _fAttackSpeed * 0.5f;
        yield return new WaitForSeconds(5.0f);
        _fAttackSpeed = fSaveAttackSpeed;
    }

    void DeathHard()                // 늑대 패시브
    {
        switch (_nPlayerHp)
        {
            case 7:
                _nFinalDmg = _nWeaponDmg;
                break;
            case 6:
                _nFinalDmg = _nWeaponDmg + ((_nWeaponDmg * 10) / 100);
                break;
            case 5:
                _nFinalDmg = _nWeaponDmg + ((_nWeaponDmg * 20) / 100);
                break;
            case 4:
                _nFinalDmg = _nWeaponDmg + ((_nWeaponDmg * 30) / 100);
                break;
            case 3:
                _nFinalDmg = _nWeaponDmg + ((_nWeaponDmg * 40) / 100);
                break;
            case 2:
                _nFinalDmg = _nWeaponDmg + ((_nWeaponDmg * 50) / 100);
                break;
            case 1:
                _nFinalDmg = _nWeaponDmg + ((_nWeaponDmg * 60) / 100);
                break;
        }
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
        //잠시끄는걸로 형이 계속 켜는걸로 하래
        //StartCoroutine(MoveMapAlphaCtrl(col));

    }
}
