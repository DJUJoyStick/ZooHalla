using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RSPEvent : MonoBehaviour
{
    enum RSP
    {
        Rock,
        Scissor,
        Paper,
        Max
    }
    public TextMeshPro GetText;
    public TextMeshPro GetWinLoseText;
    public SpriteRenderer GetRSPSprRend;
    public Sprite[] GetRSPSpr;
    public Transform[] RSPObjectTrans;
    public GameObject[] Rewards;
    public GameObject GetAniGam;

    public Animator GetRSPAni;
    private float f_Eventdistance;
    private float f_RSPdistance;
    private bool isDone;
    private bool isPlaying;
    private int RandomNum = 0;
    private Transform GetPlayer;


    private void Start()
    {
        Initial();
    }
    // Update is called once per frame
    void Update()
    {
        if (isDone && !isPlaying)
        {
            RunEvent();
        }
        EventDetectPlayer();
        
    }

    private void Initial()
    {
        GetPlayer = SGameMng.I.PlayerSc.transform;
        isDone = false;
        isPlaying = false;
        f_Eventdistance = 3f;
        f_RSPdistance = 1.5f;
        RandomNum = Random.Range(0, (int)RSP.Max);
        GetText.text = "Press Attack To Run Event";
    }

    private void EventDetectPlayer()
    {
        if (Detecting(transform.position, GetPlayer.position, f_Eventdistance))
        {
            if (!GetText.gameObject.activeInHierarchy)
            {
                GetText.gameObject.SetActive(true);
            }
            else
            {
                if (SGameMng.I.GetBtnMng.b_PressBtn)
                {
                    isDone = true;
                    GetText.text = "Already Done";
                }
            }


        }
        else if (!Detecting(transform.position, GetPlayer.position, f_Eventdistance) && GetText.gameObject.activeInHierarchy)
        {
            GetText.gameObject.SetActive(false);

        }
    }

    private bool Detecting(Vector2 pos1, Vector2 pos2, float distance)
    {
        float tempdis = Vector2.Distance(pos1, pos2);
        if (tempdis <= distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void RunEvent()
    {
        if (Detecting(RSPObjectTrans[(int)RSP.Rock].position, GetPlayer.position, f_RSPdistance) ||
            Detecting(RSPObjectTrans[(int)RSP.Scissor].position, GetPlayer.position, f_RSPdistance) ||
            Detecting(RSPObjectTrans[(int)RSP.Paper].position, GetPlayer.position, f_RSPdistance))
        {
            if (SGameMng.I.GetBtnMng.b_PressBtn)
            {
                if (Detecting(RSPObjectTrans[RandomNum].position, GetPlayer.position, f_RSPdistance))
                {

                    Destroy(GetRSPAni);
                    if (RandomNum.Equals((int)RSP.Rock))
                    {
                        GetRSPSprRend.sprite = GetRSPSpr[(int)RSP.Scissor];
                    }
                    else if (RandomNum.Equals((int)RSP.Scissor))
                    {
                        GetRSPSprRend.sprite = GetRSPSpr[(int)RSP.Paper];
                    }
                    else if (RandomNum.Equals((int)RSP.Paper))
                    {
                        GetRSPSprRend.sprite = GetRSPSpr[(int)RSP.Rock];
                    }
                    GetWinLoseText.text = "You Win!";
                    for (int i = 0; i < Rewards.Length; i++)
                    {
                        var temp = Instantiate(GetAniGam, Vector2.zero, Quaternion.identity);
                        Instantiate(Rewards[i], transform.position, Quaternion.identity, temp.transform);
                    }
                    isPlaying = true;

                }
                else
                {
                    for (int i = 0; i < (int)RSP.Max; i++)
                    {
                        if (Detecting(RSPObjectTrans[i].position, GetPlayer.position, f_RSPdistance))
                        {
                            Destroy(GetRSPAni);
                            if (i.Equals(0))
                            {
                                i = 2;
                            }
                            else
                            {
                                i--;
                            }
                            GetRSPSprRend.sprite = GetRSPSpr[i];
                            GetWinLoseText.text = "You Lose!";
                            isPlaying = true;
                            break;

                        }
                    }
                }
            }
        }


    }
}
