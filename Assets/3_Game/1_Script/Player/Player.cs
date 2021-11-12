using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerMng
{
    public GameObject BulletPre;
    public GameObject Wolf_AuraGams;

    public Transform JoyStickTr;
    public Transform GunParentTr;
    public Transform GunTr;

    public SpriteRenderer Wolf_AuraSr;

    public PlayerMapPosition GetMapPlayer;

    public Animator Wolf_AuraAnime;

    public UnityEngine.UI.Image AttackGams;
    public UnityEngine.UI.Image SkillGams;

    Coroutine RatPassiveCor;

    Vector2 PlayerMoveVec;
    Vector2 JoystickVec;


    int nSaveHealth;

    const float correction = 90f * Mathf.Deg2Rad;
    float fRotateDegree;
    float fBulletDelay;
    float fGunRot;

    bool bWolfSkillAccess = false;
    bool bRatPassive = false;
    bool bPush = false;
    bool bAniAccess = false;
    bool b = false;

    void Start()
    {
        if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
        {
            PlayerInit(ItemType.RANGED_WEAPON, 10.0f, 5, 5, true, true);
            RatPassiveCor = StartCoroutine(AutoHealth());
        }
        else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
            PlayerInit(ItemType.RANGED_WEAPON, 5.0f, 10, 10, true, true);
        else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
            PlayerInit(ItemType.RANGED_WEAPON, 7.0f, 7, 7, true, true);

        JoyStickTr = GameObject.Find("Stick").transform;
        AttackGams = GameObject.Find("AttackBtn").GetComponent<UnityEngine.UI.Image>();
        SkillGams = GameObject.Find("ActiveSkillBtn").GetComponent<UnityEngine.UI.Image>();
        GetMapPlayer = GameObject.Find("LevelGenerator").GetComponent<PlayerMapPosition>();
        _bAttackDel = true;
        _bWeaponAniOn = true;
        _fSaveSpeed = _fMoveSpeed;
        _animatorOverrideController = new AnimatorOverrideController(_PlayerWeaponAnime.runtimeAnimatorController);
        _PlayerWeaponAnime.runtimeAnimatorController = _animatorOverrideController;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            b = false;
        if (Input.GetKeyDown(KeyCode.Z))
            b = true;

        PlayerState();

        if (SGameMng.I.bMobileOn)
        {
            getKey();
            movement();
            rotation();
            if (SGameMng.I.fBossDis <= 7.0f && SGameMng.I.bBossStage)
                bossrotation();
            PlayerSkill();
        }
        else
        {
            if (_nPlayerHp > 0)
            {
                Move();
                WeaponRot();
                PlayerSkill();
            }
            Ani();
            if (Input.GetKeyDown(KeyCode.E))
                SGameMng.I.BtnMngSc.ActiveSkillBtn();
            if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    Wolf_AuraSr.flipX = false;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    Wolf_AuraSr.flipX = true;
                }
            }
        }
        if (_bAttackAccess)
            Attack();
        //else
        //{
        if (!bAniAccess)
        {
            if (_PlayerWeaponAnime.GetCurrentAnimatorStateInfo(0).IsName("NONE"))
                _PlayerWeaponAnime.enabled = false;
        }

        if (Time.time > fBulletDelay + _fAttackSpeed)
        {
            _bBulletShooting = false;
            _bWeaponAniOn = true;
        }
        if (SGameMng.I.bJoystickDown && !_bPlayerDie)
        {
            if (JoyStickTr.localPosition.x < 0)
            {
                if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
                    _bLookRight = false;
                else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
                {
                    if (!_bSkillOn)
                        _bLookRight = false;
                }
                else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
                {
                    _bLookRight = false;
                    if (Wolf_AuraGams.activeSelf)
                    {
                        Wolf_AuraAnime.SetBool("Walking", true);
                        Wolf_AuraSr.flipX = false;
                    }
                }
                _PlayerSr.flipX = false;
                _PlayerWalkAnime(true, false);
                if (SGameMng.I.fTargetDis > 7.0f || SGameMng.I.TargetEnemyTr.Equals(null))
                    _PlayerWeaponSr.flipY = false;
                else if (SGameMng.I.fTargetDis <= 7.0f)
                {
                    if (!SGameMng.I.TargetEnemyTr.Equals(null))
                    {
                        if (SGameMng.I.TargetEnemyTr.position.x < transform.position.x)
                            _PlayerWeaponSr.flipY = false;
                        else if (SGameMng.I.TargetEnemyTr.position.x > transform.position.x)
                            _PlayerWeaponSr.flipY = true;
                    }
                }
            }
            else if (JoyStickTr.localPosition.x > 0)
            {
                if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
                    _bLookRight = true;
                else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
                {
                    if (!_bSkillOn)
                        _bLookRight = true;
                }
                else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
                {
                    _bLookRight = true;
                    if (Wolf_AuraGams.activeSelf)
                    {
                        Wolf_AuraAnime.SetBool("Walking", true);
                        Wolf_AuraSr.flipX = true;
                    }
                }
                _PlayerSr.flipX = false;
                _PlayerWalkAnime(false, true);
                if (SGameMng.I.fTargetDis > 7.0f || SGameMng.I.TargetEnemyTr.Equals(null))
                    _PlayerWeaponSr.flipY = true;
                else if (SGameMng.I.fTargetDis <= 7.0f)
                {
                    if (!SGameMng.I.TargetEnemyTr.Equals(null))
                    {
                        if (SGameMng.I.TargetEnemyTr.position.x < transform.position.x)
                            _PlayerWeaponSr.flipY = false;
                        else if (SGameMng.I.TargetEnemyTr.position.x > transform.position.x)
                            _PlayerWeaponSr.flipY = true;
                    }
                }
            }
        }
        else if (!SGameMng.I.bJoystickDown)
        {
            _PlayerAnime.SetBool("isIdle", true);
            _PlayerWalkAnime(false, false);
            if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
            {
                Wolf_AuraAnime.SetBool("Idle", true);
                Wolf_AuraAnime.SetBool("Walking", false);
                if (_bLookRight)
                    Wolf_AuraSr.flipX = true;
                else
                    Wolf_AuraSr.flipX = false;
            }
        }
        else
            _PlayerWalkAnime(false, false);

        if (_nPlayerHp <= 0)
        {
            if (JoyStickTr.localPosition.x > 0 && !_PlayerAnime.GetBool("isDying"))
                _PlayerSr.flipX = true;
            _PlayerAnime.SetBool("isDying", true);
            if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
                Wolf_AuraGams.SetActive(false);
        }
    }

    void Ani()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _PlayerAnime.SetBool("isIdle", false);
            if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
            {
                if (_bSkillOn)
                {
                    Wolf_AuraAnime.SetBool("Idle", false);
                }
                //if (SGameMng.I.PlayerSc.Wolf_AuraAnime.GetBool("RIdle"))
                //    SGameMng.I.PlayerSc.Wolf_AuraAnime.SetBool("RIdle", false);
            }

            if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
                _bLookRight = false;
            else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
            {
                if (!_bSkillOn)
                    _bLookRight = false;
            }
            else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
            {
                _bLookRight = false;
                if (Wolf_AuraGams.activeSelf)
                {
                    Wolf_AuraAnime.SetBool("Walking", true);
                    Wolf_AuraSr.flipX = false;
                }
            }
            _PlayerSr.flipX = false;

            _PlayerWalkAnime(true, false);
            if (SGameMng.I.fTargetDis > 7.0f || SGameMng.I.TargetEnemyTr.Equals(null))
                _PlayerWeaponSr.flipY = false;
            else if (SGameMng.I.fTargetDis <= 7.0f)
            {
                if (!SGameMng.I.TargetEnemyTr.Equals(null))
                {
                    if (SGameMng.I.TargetEnemyTr.position.x < transform.position.x)
                        _PlayerWeaponSr.flipY = false;
                    else if (SGameMng.I.TargetEnemyTr.position.x > transform.position.x)
                        _PlayerWeaponSr.flipY = true;
                }
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            _PlayerAnime.SetBool("isIdle", false);
            if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
            {
                if (_bSkillOn)
                {
                    Wolf_AuraAnime.SetBool("Idle", false);
                    //SGameMng.I.PlayerSc.Wolf_AuraAnime.SetBool("RIdle", false);
                }
                //if (SGameMng.I.PlayerSc.Wolf_AuraAnime.GetBool("RIdle"))
                //    SGameMng.I.PlayerSc.Wolf_AuraAnime.SetBool("RIdle", false);
            }

            if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
                _bLookRight = true;
            else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
            {
                if (!_bSkillOn)
                    _bLookRight = true;
            }
            else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
            {
                _bLookRight = true;
                if (Wolf_AuraGams.activeSelf)
                {
                    Wolf_AuraAnime.SetBool("Walking", true);
                    Wolf_AuraSr.flipX = true;
                }
            }
            _PlayerSr.flipX = false;
            _PlayerWalkAnime(false, true);
            if (SGameMng.I.fTargetDis > 7.0f || SGameMng.I.TargetEnemyTr.Equals(null))
                _PlayerWeaponSr.flipY = true;
            else if (SGameMng.I.fTargetDis <= 7.0f)
            {
                if (!SGameMng.I.TargetEnemyTr.Equals(null))
                {
                    if (SGameMng.I.TargetEnemyTr.position.x < transform.position.x)
                        _PlayerWeaponSr.flipY = false;
                    else if (SGameMng.I.TargetEnemyTr.position.x > transform.position.x)
                        _PlayerWeaponSr.flipY = true;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            if (!_bPlayerDie)
            {

                _PlayerAnime.SetBool("isIdle", true);
                if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
                    Wolf_AuraAnime.SetBool("Idle", true);
                _PlayerWalkAnime(false, false);
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (!_bPlayerDie)
            {
                //if (_stickTransform.localPosition.x < 0.0f)
                //{
                //	//SGameMng.I.PlayerSc._PlayerSr.flipX = false;
                //	SGameMng.I.PlayerSc._PlayerAnime.SetBool("isIdle", true);
                //}
                //else if (_stickTransform.localPosition.x > 0.0f)
                //{
                //	SGameMng.I.PlayerSc._PlayerAnime.SetBool("isIdle", true);
                //	//SGameMng.I.PlayerSc._PlayerSr.flipX = true;
                //}
                _PlayerAnime.SetBool("isIdle", true);
                if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
                    Wolf_AuraAnime.SetBool("Idle", true);
                _PlayerWalkAnime(false, false);
            }

        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            if (!_bPlayerDie)
            {
                _PlayerWalkAnime(false, false);
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            if (!_bPlayerDie)
            {
                _PlayerWalkAnime(false, false);
            }
        }
    }

    void PlayerInit(ItemType w_type, float movespeed, int hp, int fullhp, bool moveaccess, bool dmgacces)
    {
        _PlayerWeaponType = w_type;
        _fMoveSpeed = movespeed;
        _nPlayerHp = hp;
        _nFullHp = fullhp;
        _bMoveAccess = moveaccess;
        _bDmgAccess = dmgacces;
        //WeaponSetting(_PlayerWeaponType, _PlayerWeapon);
    }

    void PlayerState()
    {
        if (_nPlayerHp <= 0)
        {
            _bPlayerDie = true;
            _PlayerSr.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            GunParentTr.gameObject.SetActive(false);
            if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
                StopCoroutine(RatPassiveCor);
        }

        if (_bPlayerDie)
        {
            SGameMng.I.nGameOver = 2;
            _bMoveAccess = false;
            _bDmgAccess = false;
            _PlayerRig.constraints = RigidbodyConstraints2D.FreezeAll;
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
        if (_PlayerWeapon.Equals(ItemPrivateNum.ENDOFCENTURYGAUNTLET))
        {
            switch (_nBulletAmount)
            {
                case 4:
                    _animatorOverrideController["Weapon_Attack"] = _PlayerWeaponAttackAniClip[24];
                    break;
                case 3:
                    _animatorOverrideController["Weapon_Attack"] = _PlayerWeaponAttackAniClip[25];
                    break;
                case 2:
                    _animatorOverrideController["Weapon_Attack"] = _PlayerWeaponAttackAniClip[26];
                    break;
                case 1:
                    _animatorOverrideController["Weapon_Attack"] = _PlayerWeaponAttackAniClip[27];
                    break;
                case 0:
                    _animatorOverrideController["Weapon_Attack"] = _PlayerWeaponAttackAniClip[28];
                    break;
            }
        }
    }

    void Move()
    {
        if (_bMoveAccess)
        {
            PlayerMoveVec = Vector2.zero;
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                JoystickVec = new Vector2(-1, 1).normalized;
            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                JoystickVec = new Vector2(1, 1).normalized;
            }
            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                JoystickVec = new Vector2(-1, -1).normalized;
            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                JoystickVec = new Vector2(-1, 1).normalized;
            }
            if (Input.GetKey(KeyCode.W))
            {
                if (_bLookRight)
                    _PlayerWalkAnime(false, true);
                else
                    _PlayerWalkAnime(true, false);
                PlayerMoveVec.y += _fMoveSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (_bLookRight)
                    _PlayerWalkAnime(false, true);
                else
                    _PlayerWalkAnime(true, false);
                PlayerMoveVec.y -= _fMoveSpeed;
            }
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
            if (Input.GetKeyDown(KeyCode.W))
            {
                bPush = true;
                JoystickVec = Vector2.up.normalized;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                bPush = true;
                JoystickVec = Vector2.down.normalized;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                bPush = true;
                JoystickVec = Vector2.left.normalized;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                JoystickVec = Vector2.right.normalized;
                bPush = true;
            }
            if (JoyStickTr.localPosition.x < -75)
                JoyStickTr.localPosition = new Vector2(-75.0f, JoyStickTr.localPosition.y);
            if (JoyStickTr.localPosition.x > 75)
                JoyStickTr.localPosition = new Vector2(75.0f, JoyStickTr.localPosition.y);
            if (JoyStickTr.localPosition.y < -75)
                JoyStickTr.localPosition = new Vector2(JoyStickTr.localPosition.x, -75);
            if (JoyStickTr.localPosition.y > 75)
                JoyStickTr.localPosition = new Vector2(JoyStickTr.localPosition.x, 75);
            if (Input.GetKeyUp(KeyCode.W))
                bPush = false;
            if (Input.GetKeyUp(KeyCode.S))
                bPush = false;
            if (Input.GetKeyUp(KeyCode.A))
                bPush = false;
            if (Input.GetKeyUp(KeyCode.D))
                bPush = false;
            if (bPush)
                JoyStickTr.Translate(JoystickVec * 500.0f * Time.deltaTime);
            else
                JoyStickTr.localPosition = Vector2.zero;
            _PlayerRig.velocity = PlayerMoveVec;
        }
        if (Input.GetMouseButton(0) && !_bPlayerDie && !b)
        {
            Attack();
            AttackGams.color = new Color(75 / 255f, 75 / 255f, 75 / 255f, 255 / 255f);
            bAniAccess = true;
            _PlayerWeaponAnime.SetBool("isAttack", true);
            _PlayerWeaponAnime.enabled = true;
        }
        if (Input.GetMouseButtonUp(0) && !_bPlayerDie)
        {
            AttackGams.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            _PlayerWeaponAnime.SetBool("isAttack", false);
            bAniAccess = false;
            //_PlayerWeaponAnime.enabled = false;
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

        GunParentTr.rotation = Quaternion.Euler(0f, 0f, fRotateDegree - 180f);

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

    void bossrotation()
    {
        if (_MoveVec.Equals(Vector3.zero))
            return;

        fGunRot = (Mathf.Atan2(_MoveVec.y, _MoveVec.x) - correction) * Mathf.Rad2Deg;
        if (_bMoveAccess)
            GunParentTr.rotation = Quaternion.Euler(0f, 0f, fGunRot - 90f);

        if (!SGameMng.I.BossTr.Equals(null))
        {
            if (SGameMng.I.bBossTarget)
            {
                float guny = SGameMng.I.BossTr.position.y - transform.position.y;
                float gunx = SGameMng.I.BossTr.position.x - transform.position.x;
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
                _bMoveAccess = false;
            else
                _bMoveAccess = true;
        }
    }

    void Attack()
    {
        if (SGameMng.I.bMobileOn)
        {
            if (!_bBulletShooting && SGameMng.I.b_WeaponSelected)
            {
                fBulletDelay = Time.time;

                if (_PlayerWeaponType.Equals(ItemType.RANGED_WEAPON))
                {
                    if (_nBulletAmount > 0)
                    {
                        if (!_bBulletReloading)
                        {
                            WeaponSoundPlay();
                            Instantiate(BulletPre, GunTr.position, Quaternion.Euler(0f, 0f, fGunRot));
                            _nBulletAmount--;
                        }
                    }
                    if (_nBulletAmount <= 0 && !_bBulletReloading)
                        StartCoroutine(WeaponReload());
                }
                else if (_PlayerWeaponType.Equals(ItemType.MELEE_WEAPON))
                {
                    if (_bAttackDel)
                    {
                        _bAttackDel = false;
                        StartCoroutine(_MeleeWeaponAttack());
                    }
                }

                _bBulletShooting = true;
            }
        }
        else
        {
            if (!_bBulletShooting && SGameMng.I.b_WeaponSelected)
            {
                fBulletDelay = Time.time;

                if (_PlayerWeaponType.Equals(ItemType.RANGED_WEAPON))
                {
                    if (_nBulletAmount > 0)
                    {
                        if (!_bBulletReloading)
                        {
                            WeaponSoundPlay();
                            Instantiate(BulletPre, GunTr.position, Quaternion.Euler(0f, 0f, fRotateDegree - 90f));
                            _nBulletAmount--;
                        }
                    }
                    if (_nBulletAmount <= 0 && !_bBulletReloading)
                        StartCoroutine(WeaponReload());
                }
                else if (_PlayerWeaponType.Equals(ItemType.MELEE_WEAPON))
                {
                    if (_bAttackDel)
                    {
                        _bAttackDel = false;
                        StartCoroutine(_MeleeWeaponAttack());
                    }
                }

                _bBulletShooting = true;
            }
        }
    }

    void TurtleSkill()              //거북이 액티브
    {

        if (!_bSkillOn)
        {
            if (SGameMng.I.CharActiveBtn.image.fillAmount < 1)
                SGameMng.I.CharActiveBtn.image.fillAmount = SGameMng.I.CharActiveBtn.image.fillAmount + 0.001f;
        }
        else
        {
            if (SGameMng.I.CharActiveBtn.image.fillAmount > 0)
                SGameMng.I.CharActiveBtn.image.fillAmount = SGameMng.I.CharActiveBtn.image.fillAmount - 0.0015f;
        }
        if (SGameMng.I.CharActiveBtn.image.fillAmount <= 0)
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
            if (!bWolfSkillAccess)
            {
                Wolf_AuraGams.SetActive(true);
                StartCoroutine(ICanStopMe());
                _bSkillOn = false;
            }
        }
    }

    public IEnumerator WeaponReload()
    {
        _bBulletShooting = true;
        _bBulletReloading = true;
        yield return new WaitForSeconds(_fReloadTime);
        //WeaponSetting(_PlayerWeaponType, _PlayerWeapon);
        _nBulletAmount = _nFullBulletAmount;
        _bBulletReloading = false;
        _bBulletShooting = false;
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

    public IEnumerator ICanStopMe()        // 늑대 액티브 코루틴
    {
        bWolfSkillAccess = true;
        float fSaveAttackSpeed = _fAttackSpeed;
        _fAttackSpeed = _fAttackSpeed * 0.5f;
        yield return new WaitForSeconds(5.0f);
        Wolf_AuraGams.SetActive(false);
        _fAttackSpeed = fSaveAttackSpeed;
        bWolfSkillAccess = false;
    }

    public IEnumerator GauntletAni()
    {
        yield return new WaitForSeconds(1.8f);
        _bGauntletAni = true;
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
                Monster colmon = col.transform.GetComponent<Monster>();
                if (!colmon.bStateOn[1])
                {
                    StartCoroutine(_DamageCtrl());
                    if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
                    {
                        _nPlayerHp -= colmon.nMonsterDmg;
                        nSaveHealth = _nPlayerHp;

                        StopCoroutine(RatPassiveCor);

                        if (bRatPassive)
                            bRatPassive = false;
                    }
                    else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
                    {
                        int nRand = Random.Range(1, 3);
                        if (nRand == 1)
                        {
                            colmon.nMonsterHp -= colmon.nMonsterDmg * 2;
                            _nPlayerHp -= 1;
                        }
                        else if (nRand == 2)
                            _nPlayerHp -= 1;
                    }
                    else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
                        _nPlayerHp -= 1;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("BossBullet"))
        {
            if (_bDmgAccess)
            {
                _nPlayerHp--;
                StartCoroutine(_DamageCtrl());
            }
        }

        if (col.CompareTag("SlowBullet"))
        {
            if (_bDmgAccess)
            {
                _nPlayerHp--;
                StartCoroutine(_DamageCtrl());
                StartCoroutine(_PlayerSlow());
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //정의부에서 col.tag를 채킹한다
        //미니맵 캐릭터 이동
        GetMapPlayer.GetMapPlayerMove(col);
        //플레이어 캐릭터 이동
        transform.Translate(GetMapPlayer.GetPlayerMove(col));
        //맵 알파값 조정
        //잠시끄는걸로 형이 계속 켜는걸로 하래
        //StartCoroutine(MoveMapAlphaCtrl(col));
        if (col.transform.CompareTag("Boss"))
        {
            if (_bDmgAccess)
            {
                StartCoroutine(_DamageCtrl());
                if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
                {
                    _nPlayerHp--;
                    nSaveHealth = _nPlayerHp;

                    StopCoroutine(RatPassiveCor);

                    if (bRatPassive)
                        bRatPassive = false;
                }
                else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
                {
                    int nRand = Random.Range(1, 3);
                    if (nRand == 1)
                    {
                        Monster colmon = col.gameObject.GetComponent<Monster>();
                        colmon.nMonsterHp -= colmon.nMonsterDmg * 2;
                        _nPlayerHp -= 1;
                    }
                    else if (nRand == 2)
                        _nPlayerHp -= 1;
                }
                else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
                    _nPlayerHp -= 1;
            }
        }

    }
}