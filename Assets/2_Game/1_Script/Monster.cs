using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Monster : MonoBehaviour
{
    public Monster SelfMonsterSc;

    public int nMonsterHp;

    public int nDotCount;                                                       // µµÆ®µ¥¹ÌÁö È½¼ö º¯¼ö

    public float fPlayerDis;
    public bool bThisTarget = false;
    public bool bFindMobOn = false;
    public bool bDebuffOn = false;

    // Start is called before the first frame update
    void Start()
    {
        SelfMonsterSc = this;
        SGameMng.I.FindMobList.Add(this);
        nMonsterHp = 10;
        nDotCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MonsterState();
    }

    void MonsterState()
    {

        fPlayerDis = Vector2.Distance(transform.position, SGameMng.I.RatSc.transform.position);

        if(SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
            fPlayerDis = Vector2.Distance(transform.position, SGameMng.I.RatSc.transform.position);
        else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
            fPlayerDis = Vector2.Distance(transform.position, SGameMng.I.TurtleSc.transform.position);
        //else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
        //    fPlayerDis = Vector2.Distance(transform.position, SGameMng.I.WolfSc.transform.position);


        if (nMonsterHp <= 0)
        {
            Destroy(gameObject);
        }

        if (fPlayerDis <= 7.0f)
        {
            bFindMobOn = true;
        }
        else
            bFindMobOn = false;
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
