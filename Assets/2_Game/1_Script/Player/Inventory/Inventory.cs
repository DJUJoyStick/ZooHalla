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
    [SerializeField]
    //20칸으로 늘릴 아이템이 있답니다 ㅎㅎ; 지금은 생각하지 맙시다
    private int InvenMax = 10;
    private int InvenNum = 0;



    public bool AddItem(Item GetItem)
    {
        InvenNum = CheckSlot();
        if (!InvenNum.Equals(-1))
        {
            //InvenItem[InvenNum] = GetItem;//리스트 삽입
            var ItemInfo = Instantiate(ItemIcon, GetSlots[InvenNum]);//아이콘 생성
            //아이템 이미지 설정
            ItemInfo.GetComponent<Image>().sprite = GetItem.S_Icon;
            //아이템 아이콘에 정보 보내기
            ItemInfo.GetComponent<ItemBtn>().GetInfo(GetItem, InvenNum);
            //슬롯의 콜라이더를 끄기
            GetSlots[InvenNum].gameObject.GetComponent<BoxCollider2D>().enabled = false;

            return true;
        }
        else
        {
            Debug.Log("No Slot");
            return false;
        }

    }

    //외부로부터 슬롯의 참거짓을 설정해주는 함수
    public void SetSlot(int num, bool bcheck)
    {
        bItemSlotCheck[num] = bcheck;
    }

    //슬롯이 빠지면 그 자리에 0을 집어넣자
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

    //itembtn클래스에서 참조 
    public void SlotCollOff(int slot)
    {
        GetSlots[slot].GetComponent<BoxCollider2D>().enabled = false;
    }

    public void SlotCollOn(int slot)
    {
        GetSlots[slot].GetComponent<BoxCollider2D>().enabled = true;
    }

}
