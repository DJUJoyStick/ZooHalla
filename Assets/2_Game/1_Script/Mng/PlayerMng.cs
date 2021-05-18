using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMng : MonoBehaviour
{
    private Image C_GetMapColor;

    public PLAYERTYPE _Playertype;
    public WEAPONRATING _PlayerWeaponRating;
    public WEAPONTYPE _PlayerWeaponType;
    public MELEEWEAPON _PlayerMeleeWeapon;
    public RANGEDWEAPON _PlayerRangedWeapon;

    public int _nPlayerHp;
    public int _nFullHp;                            // 최대 체력
    public int _nBulletAmount;
    public int _nFullBulletAmount;                  // 최대 총알
    public int _nEvasion;                           // 회피율
    public int _nWeaponDmg;

    public float _fMoveSpeed;
    public float _fReloadTime;
    public float _fAttackSpeed;
    private float _fAlphaDuration = 0.5f;
    private float _fDuration = 2f;
    private float _fSmoothness = 0.02f;

    public bool _bDmgAccess = false;
    public bool _bMoveAccess = false;
    public bool _bAttackAccess = false;
    public bool _bBulletShooting = false;
    public bool _bPlayerDie = false;
    public bool _bBulletReloading = false;
    public bool _bSkillOn = false;

    public IEnumerator DoorToNextStage()
    {
        _bMoveAccess = false;
        SGameMng.I.PlayerSc.PlayerRig.velocity = Vector2.zero;
        yield return new WaitForSeconds(1.0f);
        _bMoveAccess = true;
    }

    //맵 이동시 맵 스프라이트 알파값 조정
    //맵 이동시 맵 스프라이트 알파값 조정
    public IEnumerator MoveMapAlphaCtrl()
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

    public void WeaponSetting(WEAPONTYPE WeaponType)
    {
        if (WeaponType.Equals(WEAPONTYPE.MELEE_WEAPON))
        {
            switch (_PlayerMeleeWeapon)
            {
                case MELEEWEAPON.TOOTH_PICK:
                    MeleeWeaponSetting("현재무기 : 이쑤시개", WEAPONRATING.NORMAL, 4, 0.3f);
                    break;
                case MELEEWEAPON.CLUB:
                    MeleeWeaponSetting("현재무기 : 클럽", WEAPONRATING.NORMAL, 6, 0.5f);
                    break;
                case MELEEWEAPON.STONE_SPEAR:
                    MeleeWeaponSetting("현재무기 : 돌창", WEAPONRATING.NORMAL, 7, 0.5f);
                    break;
                case MELEEWEAPON.VINE_WHIP:
                    MeleeWeaponSetting("현재무기 : 넝쿨채찍", WEAPONRATING.NORMAL, 6, 0.4f);
                    break;
                case MELEEWEAPON.WOOD_SHIELD:
                    MeleeWeaponSetting("현재무기 : 나무방패", WEAPONRATING.NORMAL, 4, 0.6f);
                    break;
                case MELEEWEAPON.SPIRAL_SWORD:
                    MeleeWeaponSetting("현재무기 : 나선검", WEAPONRATING.RARE, 9, 0.5f);
                    break;
                case MELEEWEAPON.RIGHT_SWORD_LEFT_SHILED:
                    MeleeWeaponSetting("현재무기 : 오른쪽엔 검 왼쪽엔 방패", WEAPONRATING.RARE, 9, 0.5f);
                    break;
                case MELEEWEAPON.KOLA:
                    MeleeWeaponSetting("현재무기 : Kola", WEAPONRATING.RARE, 11, 0.4f);
                    break;
                case MELEEWEAPON.RAPIER:
                    MeleeWeaponSetting("현재무기 : 레이피어", WEAPONRATING.RARE, 8, 0.2f);
                    break;
                case MELEEWEAPON.FORK:
                    MeleeWeaponSetting("현재무기 : 포크", WEAPONRATING.UNIQUE, 14, 0.4f);
                    break;
                case MELEEWEAPON.SEALED_KEY:
                    MeleeWeaponSetting("현재무기 : 봉인된 열쇠", WEAPONRATING.UNIQUE, 17, 0.3f);
                    break;
                case MELEEWEAPON.MASTER_KEY:
                    MeleeWeaponSetting("현재무기 : 마스터키", WEAPONRATING.LEGEND, 35, 0.3f);
                    break;
                case MELEEWEAPON.GAUNTLET:
                    MeleeWeaponSetting("현재무기 : ??? 건틀릿", WEAPONRATING.UNKNOWN, 6, 0.4f);
                    break;
                case MELEEWEAPON.SMALL_KEY:
                    MeleeWeaponSetting("현재무기 : ??? 작은 열쇠", WEAPONRATING.UNKNOWN, 6, 0.5f);
                    break;
            }
        }
        else if (WeaponType.Equals(WEAPONTYPE.RANGED_WEAPON))
        {
            switch (_PlayerRangedWeapon)
            {
                case RANGEDWEAPON.TEST_GUN:
                    RangedWeaponSetting("현재무기 : TestGun", WEAPONRATING.NORMAL, 30, 30, 5.0f, 1, 0.1f);
                    break;
                case RANGEDWEAPON.WOOD_SLINGSHOT:
                    RangedWeaponSetting("현재무기 : 나무새총", WEAPONRATING.NORMAL, 15, 90, 1.0f, 3, 0.4f);
                    break;
                case RANGEDWEAPON.WOOD_BOW:
                    RangedWeaponSetting("현재무기 : 나무활", WEAPONRATING.NORMAL, 10, 100, 1.3f, 4, 0.6f);
                    break;
                case RANGEDWEAPON.STING:
                    RangedWeaponSetting("현재무기 : 독침", WEAPONRATING.NORMAL, 15, 100, 0.8f, 3, 0.4f);
                    break;
                case RANGEDWEAPON.BOOMERANG:
                    RangedWeaponSetting("현재무기 : 부메랑", WEAPONRATING.NORMAL, 1, 1, 1.0f, 5, 1.0f);
                    break;
                case RANGEDWEAPON.SEEDING:
                    RangedWeaponSetting("현재무기 : 씨뿌리기", WEAPONRATING.NORMAL, 15, 80, 1.0f, 4, 0.4f);
                    break;
                case RANGEDWEAPON.FIRE_BIRD:
                    RangedWeaponSetting("현재무기 : 불새", WEAPONRATING.RARE, 10, 100, 1.2f, 6, 0.6f);
                    break;
                case RANGEDWEAPON.FIRE_LOCK:
                    RangedWeaponSetting("현재무기 : 화승총", WEAPONRATING.RARE, 5, 60, 1.7f, 9, 1.0f);
                    break;
                case RANGEDWEAPON.CHAKRAM:
                    RangedWeaponSetting("현재무기 : 차크람", WEAPONRATING.RARE, 10, 80, 0.8f, 8, 0.6f);
                    break;
                case RANGEDWEAPON.NAILGUN:
                    RangedWeaponSetting("현재무기 : 네일건", WEAPONRATING.RARE, 25, 120, 1.0f, 7, 0.4f);
                    break;
                case RANGEDWEAPON.SHILED_PISTOL:
                    RangedWeaponSetting("현재무기 : 쉴드피스톨", WEAPONRATING.UNIQUE, 20, 120, 1.0f, 10, 0.5f);
                    break;
                case RANGEDWEAPON.GENTLEMAN_UMBRELLA:
                    RangedWeaponSetting("현재무기 : 신사 우산", WEAPONRATING.UNIQUE, 20, 100, 0.7f, 8, 0.4f);
                    break;
                case RANGEDWEAPON.THREE_SIX_NINE:
                    RangedWeaponSetting("현재무기 : 369", WEAPONRATING.UNIQUE, 6, 93, 1.3f, 6, 0.5f);
                    break;
                case RANGEDWEAPON.FLAX_GUN:
                    RangedWeaponSetting("현재무기 : Flax Gun", WEAPONRATING.UNIQUE, 15, 150, 1.2f, 9, 0.3f);
                    break;
                case RANGEDWEAPON.END_OF_THE_CENTURY_GAUNTLET:
                    RangedWeaponSetting("현재무기 : 세기말 건틀릿", WEAPONRATING.LEGEND, 6, 60, 1.5f, 22, 2.0f);
                    break;
            }
        }
        
    }

    void MeleeWeaponSetting(string text, WEAPONRATING rating, int dmg, float attackspeed)
    {
        SGameMng.I.TextMngSc.NowWeaponText.text = text;
        _PlayerWeaponRating = rating;
        _nWeaponDmg = dmg;
        _fAttackSpeed = attackspeed;
    }

    void RangedWeaponSetting(string text, WEAPONRATING rating, int amount, int fullamount, float reloadtime, int dmg, float attackspeed)
    {
        SGameMng.I.TextMngSc.NowWeaponText.text = text;
        _PlayerWeaponRating = rating;
        _nBulletAmount = amount;
        _nFullBulletAmount = fullamount;
        _fReloadTime = reloadtime;
        _nWeaponDmg = dmg;
        _fAttackSpeed = attackspeed;
    }
}
