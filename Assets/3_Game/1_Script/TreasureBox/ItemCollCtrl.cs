using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollCtrl : MonoBehaviour
{
    private Item temp;
    public void CollOn()//�������� ������ ���ʵڿ� �԰����ִ� �Լ�(�ִϸ��̼�Ʈ���Ż��)
    {
        temp = GetComponentInChildren<Item>();
        temp.ItemColl();
        temp.b_CollCheck = true;
    }
}
