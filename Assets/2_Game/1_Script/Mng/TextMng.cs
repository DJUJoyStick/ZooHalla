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
        if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
        {
            HealthBarImg.fillAmount = (float)SGameMng.I.RatSc._nPlayerHp / (float)SGameMng.I.RatSc._nFullHp;
            HealthBarText.text = SGameMng.I.RatSc._nPlayerHp + " / " + SGameMng.I.RatSc._nFullHp;
        }
        else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
        {
            HealthBarImg.fillAmount = (float)SGameMng.I.TurtleSc._nPlayerHp / (float)SGameMng.I.TurtleSc._nFullHp;
            HealthBarText.text = SGameMng.I.TurtleSc._nPlayerHp + " / " + SGameMng.I.TurtleSc._nFullHp;
        }
        //else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
        //{
        //    HealthBarImg.fillAmount = (float)SGameMng.I.WolfSc._nPlayerHp / (float)SGameMng.I.WolfSc._nFullHp;
        //    HealthBarText.text = SGameMng.I.WolfSc._nPlayerHp + " / " + SGameMng.I.WolfSc._nFullHp;
        //}
    }

    void AmountState()
    {
        if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
        {
            if (SGameMng.I.RatSc._PlayerWeaponType.Equals(WEAPONTYPE.RANGED_WEAPON))
            {
                AmountText.gameObject.SetActive(true);
                AmountText.text = "Amount : " + SGameMng.I.RatSc._nBulletAmount + " / " + SGameMng.I.RatSc._nFullBulletAmount;
            }
            else if (SGameMng.I.RatSc._PlayerWeaponType.Equals(WEAPONTYPE.MELEE_WEAPON))
            {
                AmountText.gameObject.SetActive(false);
            }
        }
        else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
        {
            if (SGameMng.I.TurtleSc._PlayerWeaponType.Equals(WEAPONTYPE.RANGED_WEAPON))
            {
                AmountText.gameObject.SetActive(true);
                AmountText.text = "Amount : " + SGameMng.I.TurtleSc._nBulletAmount + " / " + SGameMng.I.TurtleSc._nFullBulletAmount;
            }
            else if (SGameMng.I.TurtleSc._PlayerWeaponType.Equals(WEAPONTYPE.MELEE_WEAPON))
            {
                AmountText.gameObject.SetActive(false);
            }
        }
        //else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
        //{
        //    if (SGameMng.I.WolfSc._PlayerWeaponType.Equals(WEAPONTYPE.RANGED_WEAPON))
        //    {
        //        AmountText.gameObject.SetActive(true);
        //        AmountText.text = "Amount : " + SGameMng.I.WolfSc._nBulletAmount + " / " + SGameMng.I.WolfSc._nFullBulletAmount;
        //    }
        //    else if (SGameMng.I.WolfSc._PlayerWeaponType.Equals(WEAPONTYPE.MELEE_WEAPON))
        //    {
        //        AmountText.gameObject.SetActive(false);
        //    }
        //}
    }
}