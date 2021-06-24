using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollCtrl : MonoBehaviour
{
    private Item temp;
    public void CollOn()//아이템이 나오고 몇초뒤에 먹게해주는 함수(애니메이션트리거사용)
    {
        temp = GetComponentInChildren<Item>();
        temp.ItemColl();
        temp.b_CollCheck = true;
    }
}
