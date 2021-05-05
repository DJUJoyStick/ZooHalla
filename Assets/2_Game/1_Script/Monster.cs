using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    FindEnemy FindEnemySc;

    public float fMonsterHp;

    public bool bThisTarget = false;
    public bool bFindMobOn = false;

    public bool bAlreadyList = false;


    // Start is called before the first frame update
    void Start()
    {
        FindEnemySc = GameObject.Find("Find_Enemy").GetComponent<FindEnemy>();
        fMonsterHp = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        MonsterState();
    }

    void MonsterState()
    {
        if (fMonsterHp <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("FindEnemy"))
        {
            if (!bAlreadyList)
            {
                SGameMng.I.FindMobList.Add(this);
                bAlreadyList = true;
                bFindMobOn = true;
            }
            else
                bFindMobOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("FindEnemy"))
        {
            if (bFindMobOn)
                bFindMobOn = false;
            if (bThisTarget)
            {
                FindEnemySc.FindNearEnemy();
                bFindMobOn = false;
                bThisTarget = false;
            }
        }
    }

}
