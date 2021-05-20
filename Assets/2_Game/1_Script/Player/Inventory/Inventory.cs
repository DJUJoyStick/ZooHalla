using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Transform[] GetSlots;
    public List<Item> InvenItem;
    public GameObject ItemIcon;

    private int invennum = 0;


    public void AddItem(Item GetItem)
    {
        InvenItem.Add(GetItem);
        Instantiate(ItemIcon, GetSlots[invennum++]).GetComponent<Image>().sprite = InvenItem[0].S_Icon;
    }

}
