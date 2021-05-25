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
    //20ĭ��� �ø� �������� �ִ�ϴ� ����; ���� ������ ���ô�
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


        InvenItem.Add(GetItem);
        Instantiate(ItemIcon, GetSlots[InvenItem.Count - 1]).GetComponent<Image>().sprite = InvenItem[InvenItem.Count - 1].S_Icon;

    }

}
