using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Transform[] GetSlots;
    //public Item[] InvenItem;
    private bool[] bItemSlotCheck = {false, false, false, false, false, false, false, false, false, false };
    public GameObject ItemIcon;
    public ItemCraft Getitemcraft;
    [SerializeField]
    //20ĭ���� �ø� �������� �ִ�ϴ� ����; ������ �������� ���ô�
    //private int InvenMax = 10;
    private int InvenNum = 0;
    public bool bCraftMode = false;
    public List<int> CraftItems;


    public bool AddItem(Item GetItem)
    {
        InvenNum = CheckSlot();
        if (!InvenNum.Equals(-1))
        {
            //InvenItem[InvenNum] = GetItem;//����Ʈ ����
            var ItemInfo = Instantiate(ItemIcon, GetSlots[InvenNum]);//������ ����
            //������ �̹��� ����
            ItemInfo.GetComponent<Image>().sprite = GetItem.S_Icon;
            //������ �����ܿ� ���� ������
            ItemInfo.GetComponent<ItemBtn>().GetInfo(GetItem, InvenNum);
            //������ �ݶ��̴��� ����
            GetSlots[InvenNum].gameObject.GetComponent<BoxCollider2D>().enabled = false;

            return true;
        }
        else
        {
            Debug.Log("No Slot");
            return false;
        }

    }

    public void ClickCraftEvent()
    {
        if (!bCraftMode)
        {
            bCraftMode = true;
            Getitemcraft.gameObject.SetActive(true);
            GetComponent<Image>().color = Color.green;
        }
        else
        {
            bCraftMode = false;
            Getitemcraft.gameObject.SetActive(false);
            GetComponent<Image>().color = Color.black;
        }
        
    }

    public void GetItemReciepe()
    {
        var getinfo = Getitemcraft.FindItem(CraftItems);
        Debug.Log(getinfo.Item1+" "+getinfo.Item2);

    }

    //�ܺηκ��� ������ �������� �������ִ� �Լ�
    public void SetSlot(int num, bool bcheck)
    {
        bItemSlotCheck[num] = bcheck;
    }

    
    private int CheckSlot()
    {
        for(int i = 0; i < bItemSlotCheck.Length; i++)
        {
            if (bItemSlotCheck[i].Equals(false))
            {
                bItemSlotCheck[i] = true;
                return i;
            }

        }
        return -1;
    }

    //public void ListSwap(int origin, int swap)
    //{
    //    Debug.Log(InvenItem[origin] + " " + InvenItem[swap]);
    //    Item temp = InvenItem[origin];
    //    InvenItem[origin);
    //    InvenItem.Insert(origin, InvenItem[swap]);
    //    InvenItem.RemoveAt(swap);
    //    InvenItem.Insert(swap, temp);
    //    Debug.Log(InvenItem[origin] + " " + InvenItem[swap]);
    //}

    //itembtnŬ�������� ���� 
    public void SlotCollOff(int slot)
    {
        GetSlots[slot].GetComponent<BoxCollider2D>().enabled = false;
    }

    public void SlotCollOn(int slot)
    {
        GetSlots[slot].GetComponent<BoxCollider2D>().enabled = true;
    }

}
