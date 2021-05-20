using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMng : MonoBehaviour
{
    [SerializeField]
    List<Item> MeleeItemList;
    public Sprite[] spr;
    public Inventory inven;
    private void Awake()
    {
        Set_Toorh_Pick((int)MELEEWEAPON.TOOTH_PICK);
    }

    public void GetItemInfo(int num)
    {
        inven.AddItem(MeleeItemList[num]);
    }

    private void Set_Toorh_Pick(int num)
    {
        
        MeleeItemList.Add(new Tooth_Pick());
        MeleeItemList[num].s_name = "ÀÌ¾¥½Ã°³";
        MeleeItemList[num].i_rating = 0;
        MeleeItemList[num].i_dmg = 4;
        MeleeItemList[num].f_attspeed = 3.5f;
        MeleeItemList[num].f_range = 1f;
        MeleeItemList[num].S_Icon = spr[num];
    }



}
