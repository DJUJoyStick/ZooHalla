using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMng : MonoBehaviour
{
    [SerializeField]
    List<Item> MeleeItemList;
    public Sprite[] spr;
    public Inventory inven;

    public GameObject GetItemAniGam;
    public GameObject[] GetItemGam;
    public GameObject GetBoxGam;//상자등급에 따라 여러개로 늘릴예정
    private Treasure_Box GetBox;
    //디버그용
    public Transform pos;
    private void Awake()
    {
        Set_Tooth_Pick((int)MELEEWEAPON.TOOTH_PICK);
        Set_Club((int)MELEEWEAPON.CLUB);
        Set_Stone_Spear((int)MELEEWEAPON.STONE_SPEAR);

        InstanTreasuerBox(pos,9);
    }
    //보물상자 드랍(정보 넘기기) rate = 상자등급
    public void InstanTreasuerBox(Transform pos, int rate)
    {
        GameObject[] temp = { GetItemGam[(int)MELEEWEAPON.TOOTH_PICK], GetItemGam[(int)MELEEWEAPON.CLUB] , GetItemGam[(int)MELEEWEAPON.TOOTH_PICK],
        GetItemGam[(int)MELEEWEAPON.TOOTH_PICK], GetItemGam[(int)MELEEWEAPON.CLUB] , GetItemGam[(int)MELEEWEAPON.TOOTH_PICK],
        GetItemGam[(int)MELEEWEAPON.TOOTH_PICK], GetItemGam[(int)MELEEWEAPON.CLUB] , GetItemGam[(int)MELEEWEAPON.TOOTH_PICK],};
        GetBox = Instantiate(GetBoxGam, pos.position, Quaternion.identity).GetComponent<Treasure_Box>();
        GetBox.GetInfo(temp, GetItemAniGam, rate);
    }

    //플레이어가 아이템과 충돌할 시(아이템을 먹을경우)
    public bool GetItemInfo(int num)
    {
        return inven.AddItem(MeleeItemList[num]);
    }

    private void Set_Tooth_Pick(int num)
    {      
        MeleeItemList.Add(new Tooth_Pick());
        InitItem(num, "이쑤시개", WEAPONRATING.NORMAL, 4, 0.3f, 1f, spr[num]);
    }
    private void Set_Club(int num)
    {
        MeleeItemList.Add(new Club());
        InitItem(num, "클럽", WEAPONRATING.NORMAL, 5, 0.5f, 1f, spr[num]);
    }
    private void Set_Stone_Spear(int num)
    {
        MeleeItemList.Add(new Stone_Spear());
        InitItem(num, "돌창", WEAPONRATING.NORMAL, 7, 0.5f, 1f, spr[num]);
    }

    void InitItem(int num, string name, WEAPONRATING rating, int dmg, float attspeed, float range, Sprite icon)
    {
        MeleeItemList[num].s_name = name;
        MeleeItemList[num].i_rating = rating;
        MeleeItemList[num].i_dmg = dmg;
        MeleeItemList[num].f_attspeed = attspeed;
        MeleeItemList[num].f_range = range;
        MeleeItemList[num].S_Icon = icon;
    }

}
