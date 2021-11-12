using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public Transform[] GetSlots;
    //public Item[] InvenItem;
    private bool[] bItemSlotCheck = {false, false, false, false, false, false, false, false, false, false };
    public GameObject ItemIcon;
    public ItemCraft Getitemcraft;
    public ItemMng GetItemmng;
    public TextMeshProUGUI GetAlertText;
    public List<Sprite> CraftItemsSpr = new List<Sprite>();
    [SerializeField]
    //private int InvenMax = 10;
    private int InvenNum = 0;
    public bool bCraftMode = false;
    public bool bisCrafting = false;
    public bool bisReciepe = false;
    public int tempcount;
    [SerializeField]
    public List<int> CraftItems = new List<int>();



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
            GetSlots[InvenNum].GetComponent<BoxCollider2D>().enabled = false;

            return true;
        }
        else
        {
            GetAlertText.color = Color.white;
            GetAlertText.text = "No Slot";
            Invoke("TextAlphaCtrl", 2f);
            return false;
        }

    }

    private void TextAlphaCtrl()
    {
        GetAlertText.color = Color.clear;
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
            CraftItems.Clear();
            Getitemcraft.gameObject.SetActive(false);
            GetComponent<Image>().color = Color.black;
        }
        
    }

    public void GetItemReciepe()
    {
        Item temp;   
        if (CraftItems.Count > 0)
        {
            var getinfo = Getitemcraft.FindItem(CraftItems);
            temp = GetItemmng.SearchingItem(getinfo.Item1, getinfo.Item2);
            if (temp == null)
            {
                GetAlertText.color = Color.white;
                GetAlertText.text = "No Reciepe";
                Invoke("TextAlphaCtrl", 2f);
                bisReciepe = true;
                
            }
            else
            {
                bisCrafting = true;
                CraftItemsSpr.Add(temp.S_Icon);
                if (!AddItem(temp))//������ �Ͽ��µ� ĭ�� ������ ���
                {
                    var Item = Instantiate(GetItemmng.GetItemAniGam, Vector2.zero, Quaternion.identity);
                    Instantiate(GetItemmng.GetItemGam[temp.OutPrivateNum()], SGameMng.I.PlayerSc.transform.position, Quaternion.identity, Item.transform);
                }
            }
        }
        else
        {
            GetAlertText.color = Color.white;
            GetAlertText.text = "No Item";
            Invoke("TextAlphaCtrl", 2f);
        }

        
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
