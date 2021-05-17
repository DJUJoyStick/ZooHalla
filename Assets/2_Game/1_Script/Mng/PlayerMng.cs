using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMng : MonoBehaviour
{
    private Image C_GetMapColor;

    public RANGEDWEAPON _PlayerWeapon;

    public int _nPlayerHp;
    public int _nBulletAmount;
    public int _nEvasion;                           // 회피율

    public float _fMoveSpeed;
    public float _fReloadTime;
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

    public void ChangeGunType()
    {
        switch (_PlayerWeapon)
        {
            case RANGEDWEAPON.TESTGUN :
                _nBulletAmount = 30;
                _fReloadTime = 5.0f;
                break;
        }
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

}
