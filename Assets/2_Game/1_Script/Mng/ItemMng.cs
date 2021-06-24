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
    public GameObject GetBoxGam;//���ڵ�޿� ���� �������� �ø�����
    private Treasure_Box GetBox;
    //����׿�
    public Transform pos;
    private void Awake()
    {
        Set_Tooth_Pick((int)MELEEWEAPON.TOOTH_PICK);
        Set_Club((int)MELEEWEAPON.CLUB);
        Set_Stone_Spear((int)MELEEWEAPON.STONE_SPEAR);

        InstanTreasuerBox(pos,9);
    }
    //�������� ���(���� �ѱ��) rate = ���ڵ��
    public void InstanTreasuerBox(Transform pos, int rate)
    {
        GameObject[] temp = { GetItemGam[(int)MELEEWEAPON.TOOTH_PICK], GetItemGam[(int)MELEEWEAPON.CLUB] , GetItemGam[(int)MELEEWEAPON.TOOTH_PICK],
        GetItemGam[(int)MELEEWEAPON.TOOTH_PICK], GetItemGam[(int)MELEEWEAPON.CLUB] , GetItemGam[(int)MELEEWEAPON.TOOTH_PICK],
        GetItemGam[(int)MELEEWEAPON.TOOTH_PICK], GetItemGam[(int)MELEEWEAPON.CLUB] , GetItemGam[(int)MELEEWEAPON.TOOTH_PICK],};
        GetBox = Instantiate(GetBoxGam, pos.position, Quaternion.identity).GetComponent<Treasure_Box>();
        GetBox.GetInfo(temp, GetItemAniGam, rate);
    }

    //�÷��̾ �����۰� �浹�� ��(�������� �������)
    public bool GetItemInfo(int num)
    {
        return inven.AddItem(MeleeItemList[num]);
    }

    private void Set_Tooth_Pick(int num)
    {      
        MeleeItemList.Add(new Tooth_Pick());
        InitItem(num, "�̾��ð�", WEAPONRATING.NORMAL, 4, 0.3f, 1f, spr[num]);
    }
    private void Set_Club(int num)
    {
        MeleeItemList.Add(new Club());
        InitItem(num, "Ŭ��", WEAPONRATING.NORMAL, 5, 0.5f, 1f, spr[num]);
    }
    private void Set_Stone_Spear(int num)
    {
        MeleeItemList.Add(new Stone_Spear());
        InitItem(num, "��â", WEAPONRATING.NORMAL, 7, 0.5f, 1f, spr[num]);
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
