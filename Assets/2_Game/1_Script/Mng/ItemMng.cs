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
        Set_Tooth_Pick((int)MELEEWEAPON.TOOTH_PICK);
        Set_Club((int)MELEEWEAPON.CLUB);
    }

    public bool GetItemInfo(int num)
    {
        return inven.AddItem(MeleeItemList[num]);
    }

    private void Set_Tooth_Pick(int num)
    {
        MeleeItemList.Add(new Tooth_Pick());
        MeleeItemList[num].s_name = "ÀÌ¾¥½Ã°³";

        MeleeItemList[num].i_rating = WEAPONRATING.NORMAL;
        MeleeItemList[num].i_dmg = 4;
        MeleeItemList[num].f_attspeed = 0.3f;
        MeleeItemList[num].f_range = 1f;
        MeleeItemList[num].S_Icon = spr[num];
    }
    private void Set_Club(int num)
    {

        MeleeItemList.Add(new Club());
        MeleeItemList[num].s_name = "Å¬·´";
        MeleeItemList[num].i_rating = WEAPONRATING.NORMAL;
        MeleeItemList[num].i_dmg = 5;
        MeleeItemList[num].f_attspeed = 0.5f;
        MeleeItemList[num].f_range = 1f;
        MeleeItemList[num].S_Icon = spr[num];
    }

    private void Set_Stone_Spear(int num)
    {

        MeleeItemList.Add(new Club());
        MeleeItemList[num].s_name = "Å¬·´";
        MeleeItemList[num].i_rating = WEAPONRATING.NORMAL;
        MeleeItemList[num].i_dmg = 5;
        MeleeItemList[num].f_attspeed = 0.5f;
        MeleeItemList[num].f_range = 1f;
        MeleeItemList[num].S_Icon = spr[num];
    }


        MeleeItemList[num]._rating = WEAPONRATING.NORMAL;
        MeleeItemList[num].i_dmg = 4;
        MeleeItemList[num].f_attspeed = 3.5f;
        MeleeItemList[num].f_range = 1.0f;
        MeleeItemList[num].S_Icon = spr[num];
    }

    private void Set_Club(int num)
    {
        MeleeItemList.Add(new Club());
        MeleeItemList[num].s_name = "Å¬·´";
        MeleeItemList[num]._rating = WEAPONRATING.NORMAL;
        MeleeItemList[num].i_dmg = 6;
        MeleeItemList[num].f_attspeed = 0.5f;
        MeleeItemList[num].f_range = 1.0f;
        MeleeItemList[num].S_Icon = spr[num];
    }


}
