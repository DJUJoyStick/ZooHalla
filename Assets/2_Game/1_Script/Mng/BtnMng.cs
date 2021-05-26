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
    public Text CharSwitchText;

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
        if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
            SGameMng.I.RatSc._bAttackAccess = true;
        else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
            SGameMng.I.TurtleSc._bAttackAccess = true;
    }

    public void AttackBtnUp()
    {
        if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
            SGameMng.I.RatSc._bAttackAccess = false;
        else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
            SGameMng.I.TurtleSc._bAttackAccess = false;
    }

    public void ActiveSkillBtn()
    {
        if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
            SGameMng.I.RatSc._bSkillOn = true;
        //else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
        //    SGameMng.I.TurtleSc._bSkillOn = true;
    }

    public void PlatformSwitchBtn()
    {
        if (!SGameMng.I.bMobileOn)
            PlatformSwtichSetActive(true);
        else
            PlatformSwtichSetActive(false);
    }

    public void CharSwitchBtn()
    {
        if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
        {
            CharSwitchText.text = "Rat";
            SGameMng.I.PlayerType = PLAYERTYPE.TURTLE;
            SGameMng.I.RatSc.gameObject.SetActive(false);
            SGameMng.I.TurtleSc.gameObject.SetActive(true);
        }
        else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
        {
            CharSwitchText.text = "Turtle";
            SGameMng.I.PlayerType = PLAYERTYPE.RAT;
            SGameMng.I.TurtleSc.gameObject.SetActive(false);
            SGameMng.I.RatSc.gameObject.SetActive(true);
        }
    }

    void PlatformSwtichSetActive(bool SwitchOn)
    {
        SGameMng.I.bMobileOn = SwitchOn;
        MobileUIGams.SetActive(SwitchOn);
        AttackBtnGams.SetActive(SwitchOn);
        RollinBtnGams.SetActive(SwitchOn);
    }
}