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
    public int _nFullHp;                            // �ִ� ü��
    public int _nBulletAmount;
    public int _nFullBulletAmount;                  // �ִ� �Ѿ�
    public int _nEvasion;                           // ȸ����
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

    //�� �̵��� �� ��������Ʈ ���İ� ����
    //�� �̵��� �� ��������Ʈ ���İ� ����
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
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : �̾��ð�";
                    _PlayerWeaponRating = WEAPONRATING.NORMAL;
                    _nWeaponDmg = 4;
                    _fAttackSpeed = 0.3f;
                    break;
                case MELEEWEAPON.CLIP:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : Ŭ��";
                    _PlayerWeaponRating = WEAPONRATING.NORMAL;
                    _nWeaponDmg = 6;
                    _fAttackSpeed = 0.5f;
                    break;
                case MELEEWEAPON.STONE_SPEAR:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : ��â";
                    _PlayerWeaponRating = WEAPONRATING.NORMAL;
                    _nWeaponDmg = 7;
                    _fAttackSpeed = 0.5f;
                    break;
                case MELEEWEAPON.VINE_WHIP:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : ����ä��";
                    _PlayerWeaponRating = WEAPONRATING.NORMAL;
                    _nWeaponDmg = 6;
                    _fAttackSpeed = 0.4f;
                    break;
                case MELEEWEAPON.WOOD_SHIELD:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : ��������";
                    _PlayerWeaponRating = WEAPONRATING.NORMAL;
                    _nWeaponDmg = 4;
                    _fAttackSpeed = 0.6f;
                    break;
                case MELEEWEAPON.SPIRAL_SWORD:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : ������";
                    _PlayerWeaponRating = WEAPONRATING.RARE;
                    _nWeaponDmg = 9;
                    _fAttackSpeed = 0.5f;
                    break;
                case MELEEWEAPON.SWORD_SHIELD:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : �ҵ彯��";
                    _PlayerWeaponRating = WEAPONRATING.RARE;
                    _nWeaponDmg = 9;
                    _fAttackSpeed = 0.5f;
                    break;
                case MELEEWEAPON.KOLA:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : Kola";
                    _PlayerWeaponRating = WEAPONRATING.RARE;
                    _nWeaponDmg = 11;
                    _fAttackSpeed = 0.4f;
                    break;
                case MELEEWEAPON.RAPIER:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : �����Ǿ�";
                    _PlayerWeaponRating = WEAPONRATING.RARE;
                    _nWeaponDmg = 8;
                    _fAttackSpeed = 0.2f;
                    break;
                case MELEEWEAPON.FORK:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : ��ũ";
                    _PlayerWeaponRating = WEAPONRATING.UNIQUE;
                    _nWeaponDmg = 14;
                    _fAttackSpeed = 0.4f;
                    break;
                case MELEEWEAPON.SEALED_KEY:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : ���ε� ����";
                    _PlayerWeaponRating = WEAPONRATING.UNIQUE;
                    _nWeaponDmg = 17;
                    _fAttackSpeed = 0.3f;
                    break;
                case MELEEWEAPON.MASTER_KEY:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : ������Ű";
                    _PlayerWeaponRating = WEAPONRATING.LEGEND;
                    _nWeaponDmg = 35;
                    _fAttackSpeed = 0.3f;
                    break;
                case MELEEWEAPON.GAUNTLET:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : ??? ��Ʋ��";
                    _PlayerWeaponRating = WEAPONRATING.UNKNOWN;
                    _nWeaponDmg = 6;
                    _fAttackSpeed = 0.4f;
                    break;
                case MELEEWEAPON.SMALL_KEY:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : ??? ��������";
                    _PlayerWeaponRating = WEAPONRATING.UNKNOWN;
                    _nWeaponDmg = 6;
                    _fAttackSpeed = 0.5f;
                    break;
            }
        }
        else if (WeaponType.Equals(WEAPONTYPE.RANGED_WEAPON))
        {
            switch (_PlayerRangedWeapon)
            {
                case RANGEDWEAPON.TEST_GUN:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : TestGun";
                    _PlayerWeaponRating = WEAPONRATING.NORMAL;
                    _nBulletAmount = 30;
                    _nFullBulletAmount = 30;
                    _fReloadTime = 5.0f;
                    _nWeaponDmg = 1;
                    _fAttackSpeed = 0.1f;
                    break;
                case RANGEDWEAPON.WOOD_SLINGSHOT:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : ��������";
                    _PlayerWeaponRating = WEAPONRATING.NORMAL;
                    _nBulletAmount = 15;
                    _nFullBulletAmount = 90;
                    _fReloadTime = 1.0f;
                    _nWeaponDmg = 3;
                    _fAttackSpeed = 0.4f;
                    break;
                case RANGEDWEAPON.WOOD_BOW:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : ����Ȱ";
                    _PlayerWeaponRating = WEAPONRATING.NORMAL;
                    _nBulletAmount = 10;
                    _nFullBulletAmount = 100;
                    _fReloadTime = 1.3f;
                    _nWeaponDmg = 4;
                    _fAttackSpeed = 0.6f;
                    break;
                case RANGEDWEAPON.STING:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : ��ħ";
                    _PlayerWeaponRating = WEAPONRATING.NORMAL;
                    _nBulletAmount = 15;
                    _nFullBulletAmount = 100;
                    _fReloadTime = 0.8f;
                    _nWeaponDmg = 3;
                    _fAttackSpeed = 0.4f;
                    break;
                case RANGEDWEAPON.BOOMERANG:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : �θ޶�";
                    _PlayerWeaponRating = WEAPONRATING.NORMAL;
                    //_nBulletAmount = 30;
                    //_nFullBulletAmount = 30;
                    //_fReloadTime = 5.0f;
                    _nWeaponDmg = 5;
                    break;
                case RANGEDWEAPON.SEEDING:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : ���Ѹ���";
                    _PlayerWeaponRating = WEAPONRATING.NORMAL;
                    _nBulletAmount = 15;
                    _nFullBulletAmount = 80;
                    _fReloadTime = 1.0f;
                    _nWeaponDmg = 4;
                    _fAttackSpeed = 0.4f;
                    break;
                case RANGEDWEAPON.FIRE_BIRD:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : �һ�";
                    _PlayerWeaponRating = WEAPONRATING.RARE;
                    _nBulletAmount = 10;
                    _nFullBulletAmount = 100;
                    _fReloadTime = 1.2f;
                    _nWeaponDmg = 6;
                    _fAttackSpeed = 0.6f;
                    break;
                case RANGEDWEAPON.FIRE_LOCK:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : ȭ����";
                    _PlayerWeaponRating = WEAPONRATING.RARE;
                    _nBulletAmount = 5;
                    _nFullBulletAmount = 60;
                    _fReloadTime = 1.7f;
                    _nWeaponDmg = 9;
                    _fAttackSpeed = 1.0f;
                    break;
                case RANGEDWEAPON.CHAKRAM:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : ��ũ��";
                    _PlayerWeaponRating = WEAPONRATING.RARE;
                    _nBulletAmount = 10;
                    _nFullBulletAmount = 80;
                    _fReloadTime = 0.8f;
                    _nWeaponDmg = 8;
                    _fAttackSpeed = 0.6f;
                    break;
                case RANGEDWEAPON.RAILGUN:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : ���ϰ�";
                    _PlayerWeaponRating = WEAPONRATING.RARE;
                    _nBulletAmount = 25;
                    _nFullBulletAmount = 120;
                    _fReloadTime = 1.0f;
                    _nWeaponDmg = 7;
                    _fAttackSpeed = 0.4f;
                    break;
                case RANGEDWEAPON.SHILED_PISTOL:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : �����ǽ���";
                    _PlayerWeaponRating = WEAPONRATING.UNIQUE;
                    _nBulletAmount = 20;
                    _nFullBulletAmount = 120;
                    _fReloadTime = 1.0f;
                    _nWeaponDmg = 10;
                    _fAttackSpeed = 0.5f;
                    break;
                case RANGEDWEAPON.GENTLEMAN_UMBRELLA:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : �Ż� ���";
                    _PlayerWeaponRating = WEAPONRATING.UNIQUE;
                    _nBulletAmount = 20;
                    _nFullBulletAmount = 100;
                    _fReloadTime = 0.7f;
                    _nWeaponDmg = 8;
                    _fAttackSpeed = 0.4f;
                    break;
                case RANGEDWEAPON.THREE_SIX_NINE:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : 369";
                    _PlayerWeaponRating = WEAPONRATING.UNIQUE;
                    _nBulletAmount = 6;
                    _nFullBulletAmount = 93;
                    _fReloadTime = 1.3f;
                    _nWeaponDmg = 6;
                    _fAttackSpeed = 0.5f;
                    break;
                case RANGEDWEAPON.FLAXGUN:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : FlaxGun";
                    _PlayerWeaponRating = WEAPONRATING.UNIQUE;
                    _nBulletAmount = 15;
                    _nFullBulletAmount = 150;
                    _fReloadTime = 1.2f;
                    _nWeaponDmg = 9;
                    _fAttackSpeed = 0.3f;
                    break;
                case RANGEDWEAPON.END_OF_THE_CENTURY_GAUNTLET:
                    SGameMng.I.TextMngSc.NowWeaponText.text = "���繫�� : ���⸻ ��Ʋ��";
                    _PlayerWeaponRating = WEAPONRATING.LEGEND;
                    _nBulletAmount = 6;
                    _nFullBulletAmount = 60;
                    _fReloadTime = 1.5f;
                    _nWeaponDmg = 22;
                    _fAttackSpeed = 2.0f;
                    break;
            }
        }
        
    }
}
