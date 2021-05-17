using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public UnityEngine.UI.Image HealthBarImg;
    public UnityEngine.UI.Text HealthBarText;

    void Update()
    {
        HealthState();
    }

    void HealthState()
    {
        HealthBarImg.fillAmount = (float)SGameMng.I.PlayerSc._nPlayerHp / (float)SGameMng.I.nFullHp;
        HealthBarText.text = SGameMng.I.PlayerSc._nPlayerHp + " / " + SGameMng.I.nFullHp;
    }
}