using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Treasure_Box : MonoBehaviour
{
    public float f_distance;
    private Transform GetPlayer;
    [SerializeField]
    private GameObject[] GetItemGam;
    private GameObject GetAniGam;
    public Animator GetBoxAni;

    public TextMeshPro GetText;
    private int num;
    private bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        Initial();
    }

    private void Initial()
    {
        GetBoxAni.enabled = false;
        GetPlayer = SGameMng.I.PlayerSc.transform;
        GetText.text = "Press Attack for open Chest";
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
    }

    public void GetInfo(GameObject[] getitem, GameObject getani, int rate)
    {
        GetAniGam = getani;
        num = rate;
        GetItemGam = new GameObject[num];
        for (int i = 0; i < num; i++)
        {
            GetItemGam[i] = getitem[i];
        }
    }

    private void DestroyChest()
    {
        if (isOpen)
        {
            Destroy(gameObject);
        }

    }

    public void DetectPlayer()
    {
        float dis = Vector2.Distance(transform.position, GetPlayer.position);
        if (dis <= f_distance && !isOpen)
        {
            if (!GetText.gameObject.activeInHierarchy)
            {
                GetText.gameObject.SetActive(true);
            }
            OpenChest();
        }
        else if (dis > f_distance && GetText.gameObject.activeInHierarchy)
        {
            GetText.gameObject.SetActive(false);
        }
    }

    public void OpenChest()
    {
        if (SGameMng.I.GetBtnMng.b_PressBtn)
        {
            GetBoxAni.enabled = true;
            for (int i = 0; i < num; i++)
            {
                var temp = Instantiate(GetAniGam, Vector2.zero, Quaternion.identity);
                Instantiate(GetItemGam[i], transform.position, Quaternion.identity, temp.transform);
            }

            isOpen = true;

        }

    }
}
