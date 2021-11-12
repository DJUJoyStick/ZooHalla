using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanShapeBulletParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SGameMng.I.BulletDirectToPlayer(transform);
        StartCoroutine(SGameMng.I.BulletDestroy(gameObject));
    }
}
