using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEndParent : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SGameMng.I.BulletDestroy(gameObject));
    }

}
