using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Transform[] GetSlots;
    public List<Item> InvenItem;
    public GameObject ItemIcon;



    public void AddItem(Item GetItem)
    {
        InvenItem.Add(GetItem);
        Instantiate(ItemIcon, GetSlots[InvenItem.Count - 1]).GetComponent<Image>().sprite = InvenItem[InvenItem.Count - 1].S_Icon;
    }

}
