using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMng : MonoBehaviour
{
    public Image HealthBarImg;
    public Text HealthBarText;
    public Text NowWeaponText;
    public Text AmountText;

    void Update()
    {
        HealthState();
        AmountState();
    }

    void HealthState()
    {
        HealthBarImg.fillAmount = (float)SGameMng.I.PlayerSc._nPlayerHp / (float)SGameMng.I.PlayerSc._nFullHp;
        HealthBarText.text = SGameMng.I.PlayerSc._nPlayerHp + " / " + SGameMng.I.PlayerSc._nFullHp;
    }

    void AmountState()
    {
        if (SGameMng.I.PlayerSc._PlayerWeaponType.Equals(WEAPONTYPE.RANGED_WEAPON))
        {
            AmountText.gameObject.SetActive(true);
            AmountText.text = "Amount : " + SGameMng.I.PlayerSc._nBulletAmount + " / " + SGameMng.I.PlayerSc._nFullBulletAmount;
        }
        else if (SGameMng.I.PlayerSc._PlayerWeaponType.Equals(WEAPONTYPE.MELEE_WEAPON))
        {
            AmountText.gameObject.SetActive(false);
        }
    }
}