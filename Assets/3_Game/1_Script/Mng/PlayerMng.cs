using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMng : MonoBehaviour
{
    public GameObject _PlayerWeaponGams;

    public SpriteRenderer _PlayerSr;
    public SpriteRenderer _PlayerWeaponSr;

    public Rigidbody2D _PlayerRig;

    public BoxCollider2D _PlayerWeaponBc;

    public Animator _PlayerAnime;

    public Animator _PlayerWeaponAnime;

    public AnimationClip[] _PlayerWeaponAttackAniClip;
    public AnimatorOverrideController _animatorOverrideController;

    public Vector3 _MoveVec;                        // 모바일
    public Vector3 _RotVec;                         // 모바일

    public WEAPONRATING _PlayerWeaponRating;
    public ItemType _PlayerWeaponType;
    public ItemPrivateNum _PlayerWeapon;

    public int _nPlayerHp;
    public int _nFullHp;                            // 최대 체력
    public int _nBulletAmount;
    public int _nFullBulletAmount;                  // 최대 총알
    public int _nSaveBulletAmount;
    public int _nWeaponDmg;
    public int _nPlusDmg;                           // 추가 데미지
    public int _nFinalDmg;                          // 최종 데미지

    public float _fMoveSpeed;
    public float _fReloadTime;
    public float _fAttackSpeed;
    public float _fAniFrame;
    private float _fAlphaDuration = 0.5f;
    private float _fDuration = 2f;
    private float _fSmoothness = 0.02f;
    public float _fSaveSpeed;

    public bool _bDmgAccess = false;
    public bool _bMoveAccess = false;
    public bool _bAttackAccess = false;
    public bool _bAttackDel = false;
    public bool _bBulletShooting = false;
    public bool _bPlayerDie = false;
    public bool _bBulletReloading = false;
    public bool _bSkillOn = false;
    public bool _bLookRight = false;
    public bool _bWeaponAniOn = false;
    public bool _bGauntletAni = false;
    public bool _bSlowOn = false;

    public void getKey()
    {
        _MoveVec = new Vector3(CnControls.CnInputManager.GetAxis("Horizontal"), CnControls.CnInputManager.GetAxis("Vertical"));
        _RotVec = new Vector3(CnControls.CnInputManager.GetAxis("RotateX"), CnControls.CnInputManager.GetAxis("RotateY"));
    }

    public void movement()
    {
        if (_bMoveAccess)
            _PlayerRig.velocity = _MoveVec * _fMoveSpeed;
    }

    public IEnumerator DoorToNextStage()
    {
        _bMoveAccess = false;
        _PlayerRig.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.2f);
        _bMoveAccess = true;
    }

    public IEnumerator _DamageCtrl()
    {
        Mng.I.Play("Hit", true, false);
        _PlayerSr.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 125 / 255f);
        _bDmgAccess = false;
        yield return new WaitForSeconds(1.5f);
        _PlayerSr.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        if (!SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
            _bDmgAccess = true;
        else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE) && !_bSkillOn)
            _bDmgAccess = true;
    }

    public IEnumerator _MeleeWeaponAttack()
    {
        _PlayerWeaponBc.enabled = true;
        WeaponSoundPlay();
        yield return new WaitForSeconds(_fAttackSpeed);
        _PlayerWeaponBc.enabled = false;
        _bAttackDel = true;
    }

    public void _PlayerWalkAnime(bool lwalk, bool rwalk)
    {
        _PlayerAnime.SetBool("isLWalking", lwalk);
        _PlayerAnime.SetBool("isRWalking", rwalk);
    }

    public void _WeaponAniTrick()
    {
        _PlayerWeaponAnime.enabled = false;
    }

    //맵 이동시 맵 스프라이트 알파값 조정
    //맵 이동시 맵 스프라이트 알파값 조정
    public IEnumerator MoveMapAlphaCtrl(Collider2D GetTag)
    {
        if (GetTag.CompareTag("UpDoor") || GetTag.CompareTag("DownDoor") || GetTag.CompareTag("LeftDoor") || GetTag.CompareTag("RightDoor"))
        {
            float progress = 0f;
            float increment = _fSmoothness / _fAlphaDuration;

            while (progress < 1)
            {
                SGameMng.I.C_MapColor.GetComponent<Image>().color = Color.Lerp(Color.clear, Color.white, progress);
                progress += increment;
                yield return new WaitForSeconds(_fSmoothness);
            }

            progress = 0f;
            yield return new WaitForSeconds(_fDuration);

            while (progress < 1)
            {
                SGameMng.I.C_MapColor.GetComponent<Image>().color = Color.Lerp(Color.white, Color.clear, progress);
                progress += increment;
                yield return new WaitForSeconds(_fSmoothness);
            }
        }

    }

    public IEnumerator _PlayerSlow()
    {
        if (!_bSlowOn)
        {
            _bSlowOn = true;
            _fMoveSpeed = _fMoveSpeed * 0.7f;
        }
        yield return new WaitForSeconds(5.0f);
        _fMoveSpeed = _fSaveSpeed;
        _bSlowOn = false;
    }

    public void _FingerSnap()
    {
        int n = SGameMng.I.nMonCount / 2;
        Monster[] FingerSnapMon = new Monster[n];
        for (int i = 0; i < n; i++)
        {
            FingerSnapMon[i] = SGameMng.I.FindMobList[i];
        }
        for (int i = 0; i < FingerSnapMon.Length; i++)
        {
            Destroy(FingerSnapMon[i].gameObject);
            SGameMng.I.FindMobList.Remove(FingerSnapMon[i]);
        }
        SGameMng.I.nMonCount -= n;
    }

    public void SceneChange(string SceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
    }

    public void WeaponSetting(ItemType WeaponType, ItemPrivateNum playerweapon)
    {
        if (WeaponType.Equals(ItemType.MELEE_WEAPON))
        {
            switch (playerweapon)
            {
                case ItemPrivateNum.TOOTH_PICK:
                    MeleeWeaponSetting("현재무기 : 이쑤시개", WEAPONRATING.NORMAL, 4, 0.3f, 0, 0.04f);
                    break;
                case ItemPrivateNum.CLUB:
                    MeleeWeaponSetting("현재무기 : 클럽", WEAPONRATING.NORMAL, 6, 0.5f, 1, 0.04f);
                    break;
                case ItemPrivateNum.STONE_SPEAR:
                    MeleeWeaponSetting("현재무기 : 돌창", WEAPONRATING.NORMAL, 7, 0.5f, 2, 0.04f);
                    break;
                case ItemPrivateNum.WOOD_SHIELD:
                    MeleeWeaponSetting("현재무기 : 나무방패", WEAPONRATING.NORMAL, 4, 0.6f, 3, 0.06f);
                    break;
                case ItemPrivateNum.SPIRAL_SWORD:
                    MeleeWeaponSetting("현재무기 : 나선검", WEAPONRATING.RARE, 9, 0.5f, 4, 0.03f);
                    break;
                case ItemPrivateNum.RSHIELD_LSWORD:
                    MeleeWeaponSetting("현재무기 : 오른쪽엔 검 왼쪽엔 방패", WEAPONRATING.RARE, 9, 0.5f, 5, 0.04f);
                    break;
                case ItemPrivateNum.KOLA:
                    MeleeWeaponSetting("현재무기 : 콜라", WEAPONRATING.RARE, 11, 0.4f, 6, 0.03f);
                    break;
                case ItemPrivateNum.LAPPER:
                    MeleeWeaponSetting("현재무기 : 레이피어", WEAPONRATING.RARE, 8, 0.2f, 7, 0.03f);
                    break;
                case ItemPrivateNum.FORK:
                    MeleeWeaponSetting("현재무기 : 포크", WEAPONRATING.UNIQUE, 14, 0.4f, 8, 0.06f);
                    break;
                case ItemPrivateNum.SEALED_KEY:
                    MeleeWeaponSetting("현재무기 : 봉인된 열쇠", WEAPONRATING.UNIQUE, 17, 0.3f, 9, 0.03f);
                    break;
                case ItemPrivateNum.MASTER_KEY:
                    MeleeWeaponSetting("현재무기 : 마스터키", WEAPONRATING.LEGEND, 35, 0.3f, 10, 0.07f);
                    break;
                case ItemPrivateNum.GAUNTLET:
                    MeleeWeaponSetting("현재무기 : ??? 건틀릿", WEAPONRATING.UNKNOWN, 6, 0.4f, 11, 0.05f);
                    break;
                case ItemPrivateNum.SMALL_KEY:
                    MeleeWeaponSetting("현재무기 : ??? 작은 열쇠", WEAPONRATING.UNKNOWN, 6, 0.5f, 12, 0.03f);
                    break;
            }
        }
        else if (WeaponType.Equals(ItemType.RANGED_WEAPON))
        {
            switch (playerweapon)
            {
                case ItemPrivateNum.WOOD_SLINGSHOT:
                    RangedWeaponSetting("현재무기 : 나무새총", WEAPONRATING.NORMAL, 15, 90, 1.0f, 3, 0.4f, 13, 0.1f, 2.5f);
                    break;
                case ItemPrivateNum.WOOD_BOW:
                    RangedWeaponSetting("현재무기 : 나무활", WEAPONRATING.NORMAL, 10, 100, 1.3f, 4, 0.6f, 14, 0.06f, 1.04f);
                    break;
                case ItemPrivateNum.POISON_NEEDLE:
                    RangedWeaponSetting("현재무기 : 독침", WEAPONRATING.NORMAL, 15, 100, 0.8f, 3, 0.4f, 15, 0.04f, 1.0f);
                    break;
                case ItemPrivateNum.FIRE_BIRD:
                    RangedWeaponSetting("현재무기 : 불새", WEAPONRATING.RARE, 10, 100, 1.2f, 6, 0.6f, 16, 0.06f, 1.04f);
                    break;
                case ItemPrivateNum.HWASEUNGGUN:
                    RangedWeaponSetting("현재무기 : 화승총", WEAPONRATING.RARE, 5, 60, 1.7f, 9, 1.0f, 17, 0.06f, 0.6f);
                    break;
                case ItemPrivateNum.CHAKRAM:
                    RangedWeaponSetting("현재무기 : 차크람", WEAPONRATING.RARE, 10, 80, 0.8f, 8, 0.6f, 18, 0.0f, 0.0f);
                    break;
                case ItemPrivateNum.NAILGUN:
                    RangedWeaponSetting("현재무기 : 네일건", WEAPONRATING.RARE, 25, 120, 1.0f, 7, 0.4f, 19, 0.05f, 0.6f);
                    break;
                case ItemPrivateNum.SHIELD_PISTOL:
                    RangedWeaponSetting("현재무기 : 쉴드피스톨", WEAPONRATING.UNIQUE, 20, 120, 1.0f, 10, 0.5f, 20, 0.05f, 0.5f);
                    break;
                case ItemPrivateNum.GENTLEBRELLA:
                    RangedWeaponSetting("현재무기 : 신사 우산", WEAPONRATING.UNIQUE, 20, 100, 0.7f, 8, 0.4f, 21, 0.03f, 0.8f);
                    break;
                case ItemPrivateNum.TFFGUN:
                    RangedWeaponSetting("현재무기 : 369", WEAPONRATING.UNIQUE, 6, 93, 1.3f, 6, 0.5f, 22, 0.05f, 0.55f);
                    break;
                case ItemPrivateNum.FLAXGUN:
                    RangedWeaponSetting("현재무기 : Flax Gun", WEAPONRATING.UNIQUE, 15, 150, 1.2f, 9, 0.3f, 23, 0.03f, 0.9f);
                    break;
                case ItemPrivateNum.ENDOFCENTURYGAUNTLET:
                    RangedWeaponSetting("현재무기 : 세기말 건틀릿", WEAPONRATING.LEGEND, 5, 60, 2.0f, 22, 2.0f, 24, 0.03f, 1.0f);
                    break;
            }
        }

    }

    void MeleeWeaponSetting(string text, WEAPONRATING rating, int dmg, float attackspeed, int index, float frame)
    {
        //SGameMng.I.TextMngSc.NowWeaponText.text = text;
        _PlayerWeaponRating = rating;
        _nWeaponDmg = dmg;
        _fAttackSpeed = attackspeed;
        _fAniFrame = frame;
        _animatorOverrideController["Weapon_Attack"] = _PlayerWeaponAttackAniClip[index];
        //_PlayerWeaponSr.sprite = SGameMng.I.WeaponSp[index];
        //_PlayerWeaponAnime.enabled = false;
    }

    void RangedWeaponSetting(string text, WEAPONRATING rating, int amount, int fullamount, float reloadtime, int dmg, float attackspeed, int index, float frame, float anispeed)
    {
        //SGameMng.I.TextMngSc.NowWeaponText.text = text;
        _PlayerWeaponRating = rating;
        _nBulletAmount = amount;
        _nSaveBulletAmount = amount;
        _nFullBulletAmount = fullamount;
        _fReloadTime = reloadtime;
        _nWeaponDmg = dmg;
        _fAttackSpeed = attackspeed;
        _fAniFrame = frame;
        _animatorOverrideController["Weapon_Attack"] = _PlayerWeaponAttackAniClip[index];
        //_PlayerWeaponSr.sprite = SGameMng.I.WeaponSp[index];
        _PlayerWeaponAnime.SetFloat("AttackSpeed", anispeed);
        if (_PlayerWeapon.Equals(ItemPrivateNum.ENDOFCENTURYGAUNTLET))
            _bGauntletAni = true;
        //_PlayerWeaponAnime.enabled = false;
    }
    public void WeaponSoundPlay()
    {
        switch (_PlayerWeapon)
        {
            case ItemPrivateNum.TOOTH_PICK:
                Mng.I.Play("Tooth_Pick", true, false);
                break;
            case ItemPrivateNum.CLUB:
                Mng.I.Play("Club", true, false);
                break;
            case ItemPrivateNum.WOOD_SHIELD:
                Mng.I.Play("Wood_Shield", true, false);
                break;
            case ItemPrivateNum.SPIRAL_SWORD:
                Mng.I.Play("SpierSword", true, false);
                break;
            case ItemPrivateNum.RSHIELD_LSWORD:
                Mng.I.Play("RSLS",true, false);
                break;
            case ItemPrivateNum.KOLA:
                Mng.I.Play("Kola", true, false);
                break;
            case ItemPrivateNum.FORK:
                Mng.I.Play("Fork", true, false);
                break;
            case ItemPrivateNum.MASTER_KEY:
                Mng.I.Play("Master_Key", true, false);
                break;
            case ItemPrivateNum.GAUNTLET:
                Mng.I.Play("Gauntlet", true, false);
                break;
            case ItemPrivateNum.SMALL_KEY:
                Mng.I.Play("Small_Key", true, false);
                break;
            case ItemPrivateNum.WOOD_SLINGSHOT:
                Mng.I.Play("Sling", true, false);
                break;
            case ItemPrivateNum.WOOD_BOW:
                Mng.I.Play("Wood_Bow", true, false);
                break;
            case ItemPrivateNum.POISON_NEEDLE:
                Mng.I.Play("Poison_sting", true, false);
                break;
            case ItemPrivateNum.FIRE_BIRD:
                Mng.I.Play("Fire_Bird", true, false);
                break;
            case ItemPrivateNum.HWASEUNGGUN:
                Mng.I.Play("HSG", true, false);
                break;
            case ItemPrivateNum.NAILGUN:
                Mng.I.Play("NailGun", true, false);
                break;
            case ItemPrivateNum.SHIELD_PISTOL:
                Mng.I.Play("Shiled_Pistol", true, false);
                break;
            case ItemPrivateNum.GENTLEBRELLA:
                Mng.I.Play("Gentle_Umb", true, false);
                break;
            case ItemPrivateNum.TFFGUN:
                Mng.I.Play("TSN", true, false);
                break;
            case ItemPrivateNum.FLAXGUN:
                Mng.I.Play("FlaxGun", true, false);
                break;
            case ItemPrivateNum.ENDOFCENTURYGAUNTLET:
                Mng.I.Play("EOTGauntlet", true, false);
                break;
        }
    }
}
