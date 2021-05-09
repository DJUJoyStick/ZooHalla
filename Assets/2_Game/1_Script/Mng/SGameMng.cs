using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGameMng : MonoBehaviour
{
    private static SGameMng _Instance = null;

    public static SGameMng I
    {
        get
        {
            if (_Instance.Equals(null))
            {
                Debug.Log("Instance is null");
            }
            return _Instance;
        }
    }

    private void Awake()
    {
        Screen.SetResolution(1280, 720, true);
        _Instance = this;
    }

    public List<Monster> FindMobList = new List<Monster>();
    public Transform NearEnemyTr;
    public float fNearDis;
    public bool bMobileOn = false;

}
