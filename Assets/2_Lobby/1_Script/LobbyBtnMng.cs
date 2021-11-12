using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBtnMng : MonoBehaviour
{
    public GameObject CharSelectedPopupGams;
    public GameObject JoystickGams;
    public GameObject UnimplementedPopUpGams;
    public GameObject[] CharIdleGams;

    public void SelectedRatBtn()
    {
        PlayerSelectInit(PLAYERTYPE.RAT);
    }
    public void SelectedTurtleBtn()
    {
        PlayerSelectInit(PLAYERTYPE.TURTLE);
    }
    public void SelectedWolfBtn()
    {
        PlayerSelectInit(PLAYERTYPE.WOLF);
    }

    public void IntractionBtn()
    {
        if (Mng.I.nUnimplemented.Equals(1))
            UnimplementedPopUpGams.SetActive(true);
        else if (Mng.I.nUnimplemented.Equals(2))
        {
            CharSelectedPopupGams.SetActive(true);
            Mng.I.CharSc.InteractionBtnGams.SetActive(false);
        }
    }

    void PlayerSelectInit(PLAYERTYPE type)
    {
        Mng.I.SelectedType = type;
        CharSelectedPopupGams.SetActive(false);
        Mng.I.PortraitImg.gameObject.SetActive(true);
        JoystickGams.SetActive(true);
        Mng.I.PortraitImg.sprite = Mng.I.PortraitSp[(int)type];
        Mng.I.bSelected = true;
        for (int i = 0; i < 3; i++)
        {
            if (i.Equals((int)type))
            {
                CharIdleGams[i].SetActive(false);
                continue;
            }
            CharIdleGams[i].SetActive(true);
        }
    }
}
