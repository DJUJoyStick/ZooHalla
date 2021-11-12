using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoomTreasure : MonoBehaviour
{
    private Transform GetPlayer;
    [SerializeField]
    private GameObject[] GetItemGam;
    private GameObject GetAniGam;

    private int num;
    // Start is called before the first frame update

    // Update is called once per frame

    public void GetEndRoomInfo(GameObject[] getitem, GameObject getani, int rate)
    {
        GetAniGam = getani;
        num = rate;
        GetItemGam = new GameObject[num];
        for (int i = 0; i < num; i++)
        {
            GetItemGam[i] = getitem[i];
        }
    }

    public void DestroyChest()
    {

       Destroy(gameObject);

    }


    public void EndOpenChestAni()
    {
        for (int i = 0; i < num; i++)
        {
            var temp = Instantiate(GetAniGam, Vector2.zero, Quaternion.identity);
            Instantiate(GetItemGam[i], transform.position, Quaternion.identity, temp.transform);
        }
    }
}
