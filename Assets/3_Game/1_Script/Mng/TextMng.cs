using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMng : MonoBehaviour
{
    public Image HealthBarImg;
    public Image AmountBarImg;
    //public Text HealthBarText;
    public Text NowWeaponText;
    public Text AmountText;

    void Update()
    {
        HealthBarState();
        AmountBarState();
        AmountState();
    }

    void HealthBarState()
    {
        HealthBarImg.fillAmount = (float)SGameMng.I.PlayerSc._nPlayerHp / (float)SGameMng.I.PlayerSc._nFullHp;
        //HealthBarText.text = SGameMng.I.PlayerSc._nPlayerHp + " / " + SGameMng.I.PlayerSc._nFullHp;
    }

    void AmountBarState()
    {
        if (SGameMng.I.PlayerSc._PlayerWeaponType.Equals(ItemType.RANGED_WEAPON))
            AmountBarImg.fillAmount = (float)SGameMng.I.PlayerSc._nBulletAmount / (float)SGameMng.I.PlayerSc._nSaveBulletAmount;
        else if (SGameMng.I.PlayerSc._PlayerWeaponType.Equals(ItemType.MELEE_WEAPON))
            AmountBarImg.fillAmount = 1.0f;
    }

    void AmountState()
    {
        if (SGameMng.I.PlayerSc._PlayerWeaponType.Equals(ItemType.RANGED_WEAPON))
        {
            AmountText.gameObject.SetActive(true);
            AmountText.text = "Amount : " + SGameMng.I.PlayerSc._nBulletAmount + " / " + SGameMng.I.PlayerSc._nFullBulletAmount;
        }
        else if (SGameMng.I.PlayerSc._PlayerWeaponType.Equals(ItemType.MELEE_WEAPON))
        {
            AmountText.gameObject.SetActive(false);
        }
    }
}