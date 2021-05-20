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

    public void GetItemInfo(int num)
    {
        inven.AddItem(MeleeItemList[num]);
    }

    private void Set_Tooth_Pick(int num)
    {
        MeleeItemList.Add(new Tooth_Pick());
        MeleeItemList[num].s_name = "이쑤시개";
        MeleeItemList[num]._rating = WEAPONRATING.NORMAL;
        MeleeItemList[num].i_dmg = 4;
        MeleeItemList[num].f_attspeed = 3.5f;
        MeleeItemList[num].f_range = 1.0f;
        MeleeItemList[num].S_Icon = spr[num];
    }

    private void Set_Club(int num)
    {
        MeleeItemList.Add(new Club());
        MeleeItemList[num].s_name = "클럽";
        MeleeItemList[num]._rating = WEAPONRATING.NORMAL;
        MeleeItemList[num].i_dmg = 6;
        MeleeItemList[num].f_attspeed = 0.5f;
        MeleeItemList[num].f_range = 1.0f;
        MeleeItemList[num].S_Icon = spr[num];
    }

}
