using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnMng : MonoBehaviour
{
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
        SGameMng.I.PlayerSc._bAttackAccess = true;
    }

    public void AttackBtnUp()
    {
        SGameMng.I.PlayerSc._bAttackAccess = false;
    }

    public void ActiveSkillBtn()
    {
        SGameMng.I.PlayerSc._bSkillOn = true;
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
