using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMng : MonoBehaviour
{
    public GUNTYPE _PlayerGunType;

    public int _nPlayerHp;
    public int _nBulletAmount;
    public int _nEvasion;                           // È¸ÇÇÀ²

    public float _fMoveSpeed;
    public float _fReloadTime;

    public bool _bDmgAccess = false;
    public bool _bMoveAccess = false;
    public bool _bAttackAccess = false;
    public bool _bBulletShooting = false;
    public bool _bPlayerDie = false;
    public bool _bBulletReloading = false;
    public bool _bSkillOn = false;

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

}
