using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMng : MonoBehaviour
{
    public GUNTYPE _PlayerGunType;

    public int _nPlayerHp;
    public int _nBulletAmount;
    public int _nEvasion;                           // ȸ����

    public float _fMoveSpeed;
    public float _fReloadTime;

    public bool _bDmgAccess = false;
    public bool _bMoveAccess = false;
    public bool _bAttackAccess = false;
    public bool _bBulletShooting = false;
    public bool _bPlayerDie = false;
    public bool _bBulletReloading = false;
    public bool _bSkillOn = false;

    private Image C_GetMapColor;



    public void ChangeGunType()
    {
        switch (_PlayerGunType)
        {
            case GUNTYPE.TESTGUN:
                _nBulletAmount = 30;
                _fReloadTime = 5.0f;
                break;
        }
    }
    //�� �̵��� �� ��������Ʈ ���İ� ����
    public void MoveMapAlphaCtrl()
    {
        C_GetMapColor = SGameMng.I.C_MapColor.GetComponent<Image>();
        Debug.Log("s");
        Color AlpahZeroColor = C_GetMapColor.color;
        AlpahZeroColor.a = 0f;
        Color.Lerp(C_GetMapColor.color, AlpahZeroColor, 1f);

    }

}
