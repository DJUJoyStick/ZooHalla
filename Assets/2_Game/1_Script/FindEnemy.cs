using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemy : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //FindNearEnemy();
    }

    //public void FindNearEnemy()
    //{
    //    for (int i = 0; i < SGameMng.I.FindMobList.Count; i++)
    //    {
    //        if (SGameMng.I.FindMobList[i].bFindMobOn)
    //        {
    //            float fDistance = Vector2.Distance(SGameMng.I.FindMobList[i].transform.position, gameObject.transform.parent.position);
    //            if (SGameMng.I.NearEnemyTr.Equals(null))
    //            {
    //                NearMobCheck(i);
    //            }
    //            else
    //                SGameMng.I.fNearDis = Vector2.Distance(SGameMng.I.NearEnemyTr.position, gameObject.transform.parent.position);

    //            if (SGameMng.I.fNearDis >= fDistance)
    //            {
    //                NearMobCheck(i);
    //            }
    //            //else if(SGameMng.I.fNearDis >= 4.0f)                                          // 자동공격 범위 벗어날 시
    //            //{
    //            //    SGameMng.I.NearEnemyTr = null;
    //            //    SGameMng.I.fNearDis = 0.0f;
    //            //}
    //        }
    //    }
    //}

    //public void NearMobCheck(int nIndex)
    //{
    //    SGameMng.I.NearEnemyTr = SGameMng.I.FindMobList[nIndex].transform;
    //    SGameMng.I.FindMobList[nIndex].bThisTarget = true;
    //}
}