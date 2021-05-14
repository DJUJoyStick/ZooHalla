using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnMng : MonoBehaviour
{
    public Player PlayerSc;
    public GameObject MobileUIGams;
    public GameObject AttackBtnGams;
    public GameObject RollinBtnGams;
    public GameObject PauseSceneGams;

    public void PauseBtn()
    {
        PauseSceneGams.SetActive(true);
    }

    public void PauseExitBtn()
    {
        PauseSceneGams.SetActive(false);
    }

    public void AttackBtnDown()
    {
        PlayerSc.bAttackAccess = true;
    }

    public void AttackBtnUp()
    {
        PlayerSc.bAttackAccess = false;
    }

    public void RollinBtn()
    {
        //Debug.Log("ROLLIN");
        PlayerSc.bRollin = true;
    }

    public void PlatformSwitchBtn()
    {
        if (!SGameMng.I.bMobileOn)
            PlatformSwtichSetActive(true);   
        else
            PlatformSwtichSetActive(false);
    }

    void PlatformSwtichSetActive(bool SwtichOn)
    {
        SGameMng.I.bMobileOn = SwtichOn;
        MobileUIGams.SetActive(SwtichOn);
        AttackBtnGams.SetActive(SwtichOn);
        RollinBtnGams.SetActive(SwtichOn);
    }
}
