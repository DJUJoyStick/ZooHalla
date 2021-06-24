using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class ItemBtn : MonoBehaviour
{
    public int nSlotNum;
    private RectTransform ItemTrans;
    //private Item GetItemInfo;
    public GameObject GetItemInfoGam;
    public Text GetItemInfoText;
    private Inventory GetInven;

    private void Start()
    {
        ItemTrans = GetComponent<RectTransform>();
        GetInven = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    public void SelectedItem()
    {
        transform.position = Input.mousePosition;
        GetComponent<BoxCollider2D>().enabled = false;
        
    }

    public void GetInfo(Item item, int nSlot)
    {
        //��° �� ����
        nSlotNum = nSlot;
        //GetItemInfo = item;
        GetItemInfoText.text =
            item.s_name
            + "\n\n"
            + item.i_rating
            + " ��� \n\n"
            + item.i_dmg
            + " ������ \n\n"
            + "���ݼӵ�: "
            + item.f_attspeed
            + "\n\n";

    }

    public void ShowItemInfo()
    {
        GetItemInfoGam.SetActive(true);
        GetInven.SlotCollOn(nSlotNum);
    }
    public void CloseItemInfo()
    {
        GetItemInfoGam.SetActive(false);
    }

    public void DropedItem()
    {
        GameObject temp = GetClicked2DObject(/*1 << LayerMask.NameToLayer("ItemSlot")*/);
        if (temp != null)
        {
            if (temp.CompareTag("ItemSlot"))
            {
                transform.SetParent(temp.transform);
                temp.GetComponent<BoxCollider2D>().enabled = false;
                //GetInven.ListSwap(nSlotNum, GetNumInString(temp.name));
                GetInven.SetSlot(nSlotNum, false);
                
                nSlotNum = GetNumInString(temp.name);//���� ���Լ��ڸ� �ٲ� ���Լ��ڷ� �ٲ���

                GetInven.SetSlot(nSlotNum, true);
                GetInven.SlotCollOff(nSlotNum);
            }
        }
        else
        {
            Debug.Log("Cant Find Collider");
        }
        
        ItemTrans.anchoredPosition = Vector2.zero;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    //���ڿ����� ���ڸ� �����Ͽ� ��ȯ
    private int GetNumInString(string str)
    {
        string temp = Regex.Replace(str, @"\D", "");
        return int.Parse(temp);
    }


    private GameObject GetClicked2DObject(int layer = -1)
    {
        GameObject target = null;
        //int mask = 1 << layer;
        Vector2 pos = Input.mousePosition;
        Ray2D ray = new Ray2D(pos, Vector2.zero);
        RaycastHit2D hit;

        hit = layer == -1 ? Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity) :
                            Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layer);

        if (hit)
        {
            target = hit.collider.gameObject;
            return target;
        }
        else
        {
            return null;
        }


    }

}
