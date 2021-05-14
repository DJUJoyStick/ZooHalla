using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Monster : MonoBehaviour
{
    public float fMonsterHp;

    public float fPlayerDis;
    public bool bThisTarget = false;
    public bool bFindMobOn = false;

    public bool bAlreadyList = false;

    // Start is called before the first frame update
    void Start()
    {
        SGameMng.I.FindMobList.Add(this);
        fMonsterHp = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        MonsterState();
    }

    void MonsterState()
    {
        fPlayerDis = Vector2.Distance(transform.position, SGameMng.I.PlayerSc.transform.position);
        if (fMonsterHp <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

    }

    private void OnTriggerExit2D(Collider2D col)
    {

    }

}
