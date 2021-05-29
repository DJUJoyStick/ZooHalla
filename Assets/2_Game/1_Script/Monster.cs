using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Monster : MonoBehaviour
{
    public Monster SelfMonsterSc;

    public int nMonsterHp;

    public int nDotCount;                                                       // 도트데미지 횟수 변수

    public float fPlayerDis;
    public bool bThisTarget = false;
    public bool bFindMobOn = false;
    public bool bDebuffOn = false;

    void Start()
    {
        SelfMonsterSc = this;
        SGameMng.I.FindMobList.Add(this);
        nMonsterHp = 10;
        nDotCount = 0;
    }

    void Update()
    {
        MonsterState();
    }

    void MonsterState()
    {
        fPlayerDis = Vector2.Distance(transform.position, SGameMng.I.PlayerSc.transform.position);
        if (nMonsterHp <= 0)
        {
            Destroy(gameObject);
        }

        if (fPlayerDis <= 7.0f)
        {
            bFindMobOn = true;
        }
        else
        {
            bFindMobOn = false;
        }
    }

    public void Debuffs(PLAYERTYPE Type)
    {
        switch (Type)
        {
            case PLAYERTYPE.RAT:
                nDotCount = 10;
                StartCoroutine(DotDmg());
                break;
        }
    }

    IEnumerator DotDmg()
    {
        nDotCount--;
        nMonsterHp--;
        yield return new WaitForSeconds(1.0f);
        if (nDotCount > 0)
            StartCoroutine(DotDmg());
    }
}
