using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMng : MonoBehaviour
{
    public bool bAreaSkillOn = false;                                  

    void Update()
    {
        CheckPlayerDis();
        if (bAreaSkillOn)
        {
            PlayerAreaSkill();
            bAreaSkillOn = false;
        }
    }

    void CheckPlayerDis()
    {
        for (int i = 0; i < SGameMng.I.FindMobList.Count; i++)
        {
            for (int j = 0; j < SGameMng.I.FindMobList.Count; j++)
            {
                if (SGameMng.I.FindMobList[i].fPlayerDis > SGameMng.I.FindMobList[j].fPlayerDis && !j.Equals(i) && !SGameMng.I.FindMobList[j].Equals(null))
                {
                    SGameMng.I.TargetEnemyTr = SGameMng.I.FindMobList[j].transform;
                    SGameMng.I.fTargetDis = SGameMng.I.FindMobList[j].fPlayerDis;
                    SGameMng.I.TargetEnemySc = SGameMng.I.FindMobList[j].SelfMonsterSc;
                }
            }
        }
    }

    void PlayerAreaSkill()
    {
        for (int i = 0; i < SGameMng.I.FindMobList.Count; i++)
        {
            if (!SGameMng.I.FindMobList.Equals(null))
            {
                SGameMng.I.FindMobList[i].Debuffs(SGameMng.I.PlayerType);
            }
        }
    }
}