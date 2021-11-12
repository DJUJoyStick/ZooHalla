using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMng : MonoBehaviour
{
    public bool bAreaSkillOn = false;

    void Start() {
        Mng.I.Play("Stage_1", false, true);
    }

    void Update()
    {
        CheckPlayerDis();
        if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
        {
            if (bAreaSkillOn)
            {
                RatActive();
                bAreaSkillOn = false;
            }
        }
    }

    void CheckPlayerDis()
    {
        if (!SGameMng.I.bBossStage || !SGameMng.I.nMonCount.Equals(1))
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
                    if (SGameMng.I.nMonCount.Equals(1))
                    {
                        if (!SGameMng.I.FindMobList[i].Equals(null))
                        {
                            SGameMng.I.TargetEnemyTr = SGameMng.I.FindMobList[i].transform;
                            SGameMng.I.fTargetDis = SGameMng.I.FindMobList[i].fPlayerDis;
                            SGameMng.I.TargetEnemySc = SGameMng.I.FindMobList[i].SelfMonsterSc;
                        }

                    }
                }
            }
        }
    }

    void RatActive()
    {
        for (int i = 0; i < SGameMng.I.FindMobList.Count; i++)
        {
            if (!SGameMng.I.FindMobList.Equals(null))
            {
                SGameMng.I.FindMobList[i].DebuffList(STATE_ELIMET.POISON, 9);
            }
        }
    }
}