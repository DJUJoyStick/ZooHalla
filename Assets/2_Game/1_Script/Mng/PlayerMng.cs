using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMng : MonoBehaviour
{
    public SpriteRenderer _PlayerSr;

    public Rigidbody2D _PlayerRig;

    public Animator _PlayerAnime;

    public Vector3 _MoveVec;                        // �����
    public Vector3 _RotVec;                         // �����
    public WEAPONRATING _PlayerWeaponRating;
    public WEAPONTYPE _PlayerWeaponType;
    public MELEEWEAPON _PlayerMeleeWeapon;
    public RANGEDWEAPON _PlayerRangedWeapon;

    public int _nPlayerHp;
    public int _nFullHp;                            // �ִ� ü��
    public int _nBulletAmount;
    public int _nFullBulletAmount;                  // �ִ� �Ѿ�
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
        yield return new WaitForSeconds(1.0f);
        _bMoveAccess = true;
    }

    public IEnumerator _DamageCtrl()
    {
        _PlayerSr.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 125 / 255f);                 // �ǰݽ� �÷��̾��� ��������Ʈ ���İ� ����(��� �����̶�� �ǹ� *�ӽ���)
        _bDmgAccess = false;
        yield return new WaitForSeconds(1.5f);
        _PlayerSr.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        _bDmgAccess = true;
    }

    public void _PlayerWalkAnime(bool lwalk, bool rwalk)
    {
        _PlayerAnime.SetBool("isLWalking", lwalk);
        _PlayerAnime.SetBool("isRWalking", rwalk);
    }

    //�� �̵��� �� ��������Ʈ ���İ� ����
    //�� �̵��� �� ��������Ʈ ���İ� ����
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

    public void SceneChange(string SceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
    }

    public void WeaponSetting(WEAPONTYPE WeaponType)
    {
        if (WeaponType.Equals(WEAPONTYPE.MELEE_WEAPON))
        {
            switch (_PlayerMeleeWeapon)
            {
                case MELEEWEAPON.TOOTH_PICK:
                    MeleeWeaponSetting("���繫�� : �̾��ð�", WEAPONRATING.NORMAL, 4, 0.3f);
                    break;
                case MELEEWEAPON.CLUB:
                    MeleeWeaponSetting("���繫�� : Ŭ��", WEAPONRATING.NORMAL, 6, 0.5f);
                    break;
                case MELEEWEAPON.STONE_SPEAR:
                    MeleeWeaponSetting("���繫�� : ��â", WEAPONRATING.NORMAL, 7, 0.5f);
                    break;
                case MELEEWEAPON.VINE_WHIP:
                    MeleeWeaponSetting("���繫�� : ����ä��", WEAPONRATING.NORMAL, 6, 0.4f);
                    break;
                case MELEEWEAPON.WOOD_SHIELD:
                    MeleeWeaponSetting("���繫�� : ��������", WEAPONRATING.NORMAL, 4, 0.6f);
                    break;
                case MELEEWEAPON.SPIRAL_SWORD:
                    MeleeWeaponSetting("���繫�� : ������", WEAPONRATING.RARE, 9, 0.5f);
                    break;
                case MELEEWEAPON.RIGHT_SWORD_LEFT_SHILED:
                    MeleeWeaponSetting("���繫�� : �����ʿ� �� ���ʿ� ����", WEAPONRATING.RARE, 9, 0.5f);
                    break;
                case MELEEWEAPON.KOLA:
                    MeleeWeaponSetting("���繫�� : Kola", WEAPONRATING.RARE, 11, 0.4f);
                    break;
                case MELEEWEAPON.RAPIER:
                    MeleeWeaponSetting("���繫�� : �����Ǿ�", WEAPONRATING.RARE, 8, 0.2f);
                    break;
                case MELEEWEAPON.FORK:
                    MeleeWeaponSetting("���繫�� : ��ũ", WEAPONRATING.UNIQUE, 14, 0.4f);
                    break;
                case MELEEWEAPON.SEALED_KEY:
                    MeleeWeaponSetting("���繫�� : ���ε� ����", WEAPONRATING.UNIQUE, 17, 0.3f);
                    break;
                case MELEEWEAPON.MASTER_KEY:
                    MeleeWeaponSetting("���繫�� : ������Ű", WEAPONRATING.LEGEND, 35, 0.3f);
                    break;
                case MELEEWEAPON.GAUNTLET:
                    MeleeWeaponSetting("���繫�� : ??? ��Ʋ��", WEAPONRATING.UNKNOWN, 6, 0.4f);
                    break;
                case MELEEWEAPON.SMALL_KEY:
                    MeleeWeaponSetting("���繫�� : ??? ���� ����", WEAPONRATING.UNKNOWN, 6, 0.5f);
                    break;
            }
        }
        else if (WeaponType.Equals(WEAPONTYPE.RANGED_WEAPON))
        {
            switch (_PlayerRangedWeapon)
            {
                case RANGEDWEAPON.TEST_GUN:
                    RangedWeaponSetting("���繫�� : TestGun", WEAPONRATING.NORMAL, 30, 30, 5.0f, 1, 0.1f);
                    break;
                case RANGEDWEAPON.WOOD_SLINGSHOT:
                    RangedWeaponSetting("���繫�� : ��������", WEAPONRATING.NORMAL, 15, 90, 1.0f, 3, 0.4f);
                    break;
                case RANGEDWEAPON.WOOD_BOW:
                    RangedWeaponSetting("���繫�� : ����Ȱ", WEAPONRATING.NORMAL, 10, 100, 1.3f, 4, 0.6f);
                    break;
                case RANGEDWEAPON.STING:
                    RangedWeaponSetting("���繫�� : ��ħ", WEAPONRATING.NORMAL, 15, 100, 0.8f, 3, 0.4f);
                    break;
                case RANGEDWEAPON.BOOMERANG:
                    RangedWeaponSetting("���繫�� : �θ޶�", WEAPONRATING.NORMAL, 1, 1, 1.0f, 5, 1.0f);
                    break;
                case RANGEDWEAPON.SEEDING:
                    RangedWeaponSetting("���繫�� : ���Ѹ���", WEAPONRATING.NORMAL, 15, 80, 1.0f, 4, 0.4f);
                    break;
                case RANGEDWEAPON.FIRE_BIRD:
                    RangedWeaponSetting("���繫�� : �һ�", WEAPONRATING.RARE, 10, 100, 1.2f, 6, 0.6f);
                    break;
                case RANGEDWEAPON.FIRE_LOCK:
                    RangedWeaponSetting("���繫�� : ȭ����", WEAPONRATING.RARE, 5, 60, 1.7f, 9, 1.0f);
                    break;
                case RANGEDWEAPON.CHAKRAM:
                    RangedWeaponSetting("���繫�� : ��ũ��", WEAPONRATING.RARE, 10, 80, 0.8f, 8, 0.6f);
                    break;
                case RANGEDWEAPON.NAILGUN:
                    RangedWeaponSetting("���繫�� : ���ϰ�", WEAPONRATING.RARE, 25, 120, 1.0f, 7, 0.4f);
                    break;
                case RANGEDWEAPON.SHILED_PISTOL:
                    RangedWeaponSetting("���繫�� : �����ǽ���", WEAPONRATING.UNIQUE, 20, 120, 1.0f, 10, 0.5f);
                    break;
                case RANGEDWEAPON.GENTLEMAN_UMBRELLA:
                    RangedWeaponSetting("���繫�� : �Ż� ���", WEAPONRATING.UNIQUE, 20, 100, 0.7f, 8, 0.4f);
                    break;
                case RANGEDWEAPON.THREE_SIX_NINE:
                    RangedWeaponSetting("���繫�� : 369", WEAPONRATING.UNIQUE, 6, 93, 1.3f, 6, 0.5f);
                    break;
                case RANGEDWEAPON.FLAX_GUN:
                    RangedWeaponSetting("���繫�� : Flax Gun", WEAPONRATING.UNIQUE, 15, 150, 1.2f, 9, 0.3f);
                    break;
                case RANGEDWEAPON.END_OF_THE_CENTURY_GAUNTLET:
                    RangedWeaponSetting("���繫�� : ���⸻ ��Ʋ��", WEAPONRATING.LEGEND, 6, 60, 1.5f, 22, 2.0f);
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
