using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Transform[] GetSlots;
    public List<Item> InvenItem;
    public GameObject ItemIcon;
    [SerializeField]
    //20칸으로 늘릴 아이템이 있답니다 ㅎㅎ; 지금은 생각하지 맙시다
    private const int InvenMax = 10;
    private int InvenNum = 0;


    public bool AddItem(Item GetItem)
    {
        if(InvenNum < 10)
        {
            InvenItem.Add(GetItem);
            Instantiate(ItemIcon, GetSlots[InvenNum]).GetComponent<Image>().sprite = InvenItem[InvenNum].S_Icon;
            InvenNum++;
            return true;
        }
        else
        {
            Debug.Log("No Slot");
            return false;
        }

    }

}
