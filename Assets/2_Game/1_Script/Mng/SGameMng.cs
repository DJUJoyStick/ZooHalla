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
        _Instance = this;
        Screen.SetResolution(1280, 720, true);
    }

    public List<Monster> FindMobList = new List<Monster>();

    public Transform TargetEnemyTr;

    public UnityEngine.UI.Text testLog;
    public UnityEngine.UI.Image C_MapColor;

    public Rat PlayerSc;
    public MonsterMng MonsterMngSc;
    public Monster TargetEneymSc;

    public float fTargetDis;

    public bool bMobileOn = false;


    public void log(string msg)
    {
        testLog.text += msg + "\n";
    }
}
