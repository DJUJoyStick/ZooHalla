using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemMng : MonoBehaviour
{
    [SerializeField]
    List<Item> ItemList;
    public Inventory inven;

    public GameObject GetItemAniGam;
    public GameObject[] GetItemGam;
    public GameObject []GetBoxGam;
    private Treasure_Box GetBox;
    private EndRoomTreasure GetEndRoomBox;
    //디버그용
    public Transform pos;
    //디버그용
    private ItemPrivateNum[] prinum = {ItemPrivateNum.WOOD,ItemPrivateNum.WOOD,ItemPrivateNum.STONE,ItemPrivateNum.SHIELD_PISTOL
    ,ItemPrivateNum.FIRE_BIRD, ItemPrivateNum.STONE};
   
    private void Awake()
    {
        Set_Bigger_Potion((int)ItemPrivateNum.BIGGER_POTION);
        Set_Wood((int)ItemPrivateNum.WOOD);
        Set_Stone((int)ItemPrivateNum.STONE);
        Set_Flint((int)ItemPrivateNum.FLINT);
        Set_Vine((int)ItemPrivateNum.VINE);
        Set_Seed((int)ItemPrivateNum.SEED);
        Set_Scrap_Metal((int)ItemPrivateNum.SCRAP_METAL);
        Set_GunPowder((int)ItemPrivateNum.GUN_POWDER);
        Set_Electric_wire((int)ItemPrivateNum.ELECTRIC_WIRE);
        Set_Steel((int)ItemPrivateNum.STEEL);
        Set_Gold((int)ItemPrivateNum.GOLD);
        Set_Meteorite_Debris((int)ItemPrivateNum.METEORITE_DEBRIS);
        Set_Ruby((int)ItemPrivateNum.RUBY);
        Set_Sapphire((int)ItemPrivateNum.SAPPHIRE);
        Set_Emerald((int)ItemPrivateNum.EMERALD);
        Set_Amethyst((int)ItemPrivateNum.AMETHYST);
        Set_Amber((int)ItemPrivateNum.AMBER);
        Set_Topaz((int)ItemPrivateNum.TOPAZ);

        Set_Tooth_Pick((int)ItemPrivateNum.TOOTH_PICK);
        Set_Club((int)ItemPrivateNum.CLUB);
        Set_Stone_Spear((int)ItemPrivateNum.STONE_SPEAR);
        Set_Vine_Whipping((int)ItemPrivateNum.VINE_WHIPPING);
        Set_Wood_Shield((int)ItemPrivateNum.WOOD_SHIELD);
        Set_Spiral_Sword((int)ItemPrivateNum.SPIRAL_SWORD);
        Set_RSowrd_LShield((int)ItemPrivateNum.RSHIELD_LSWORD);
        Set_Kola((int)ItemPrivateNum.KOLA);
        Set_Lapeer((int)ItemPrivateNum.LAPPER);
        Set_Fork((int)ItemPrivateNum.FORK);
        Set_Sealed_Key((int)ItemPrivateNum.SEALED_KEY);
        Set_Master_Key((int)ItemPrivateNum.MASTER_KEY);
        Set_Gauntlet((int)ItemPrivateNum.GAUNTLET);
        Set_Small_Key((int)ItemPrivateNum.SMALL_KEY);
        Set_Wood_SlingShot((int)ItemPrivateNum.WOOD_SLINGSHOT);
        Set_Wood_Bow((int)ItemPrivateNum.WOOD_BOW);
        Set_Poison_Needle((int)ItemPrivateNum.POISON_NEEDLE);
        Set_Boomerang((int)ItemPrivateNum.BOOMERANG);
        Set_Seeding((int)ItemPrivateNum.SEEDING);
        Set_FireBird((int)ItemPrivateNum.FIRE_BIRD);
        Set_HwaseungGun((int)ItemPrivateNum.HWASEUNGGUN);
        Set_Chakram((int)ItemPrivateNum.CHAKRAM);
        Set_Nail_Gun((int)ItemPrivateNum.NAILGUN);
        Set_Shield_Pistol((int)ItemPrivateNum.SHIELD_PISTOL);
        Set_GentleBrella((int)ItemPrivateNum.GENTLEBRELLA);
        Set_TFF_Gun((int)ItemPrivateNum.TFFGUN);
        Set_Flax_Gun((int)ItemPrivateNum.FLAXGUN);
        Set_EndOfCentury_Gauntlet((int)ItemPrivateNum.ENDOFCENTURYGAUNTLET);
        Set_PineCone_Bomb((int)ItemPrivateNum.PINECON_BOMB);
        Set_Seed_Bomb((int)ItemPrivateNum.SEED_BOMB);
        Set_Robot_Toy((int)ItemPrivateNum.ROBOTTOY);
        Set_Plasma_Grenade((int)ItemPrivateNum.PLASMA_GRENADE);
        Set_Catapult((int)ItemPrivateNum.CATAPULT);
        Set_SunFlower((int)ItemPrivateNum.SUNFLOWER);
        Set_TV((int)ItemPrivateNum.TV);
        Set_Stone_Harubang((int)ItemPrivateNum.STONE_HARUBANG);
        Set_MiniGun_Turret((int)ItemPrivateNum.MINIGUN_TURRET);

        Set_Apple((int)ItemPrivateNum.APPLE);
        Set_Banana((int)ItemPrivateNum.BANANA);
        Set_Carrot((int)ItemPrivateNum.CARROT);
        Set_Strawberry((int)ItemPrivateNum.STRAWBERRY);
        Set_Orange((int)ItemPrivateNum.ORANGE);
        Set_Blueberry((int)ItemPrivateNum.BLUEBERRY);
        Set_Green_Apple((int)ItemPrivateNum.GREEN_APPLE);
        Set_Mango((int)ItemPrivateNum.MANGO);
        Set_Dragon_Fruit((int)ItemPrivateNum.DRAGON_FRUIT);
        Set_Red_Potion((int)ItemPrivateNum.RED_POTION);
        Set_Yellow_Potion((int)ItemPrivateNum.YELLOW_POTION);
        Set_Blue_Potion((int)ItemPrivateNum.BLUE_POTION);
        Set_Giant_Potion((int)ItemPrivateNum.GIANT_POTION);
        Set_Wood_Wall((int)ItemPrivateNum.WOOD_WALL);
        Set_Stone_Wall((int)ItemPrivateNum.STONE_WALL);
        Set_Rugged_IronWall((int)ItemPrivateNum.RUGGED_IRONWALL);
        Set_Iron_Wall((int)ItemPrivateNum.IRON_WALL);
        Set_CarrotApple_Juice((int)ItemPrivateNum.CARROTAPPLE_JUICE);
        Set_BlueberryMango_Smoothie((int)ItemPrivateNum.BLUEBERRYMANGO_SMOOTHIE);
        Set_Doubleberry((int)ItemPrivateNum.DOUBLEBERRY);
        Set_StrawberryBanana_Shake((int)ItemPrivateNum.STRAWBERRYBANANA_SHAKE);
        Set_Sorry_Potion((int)ItemPrivateNum.SORRY_POTION);
        Set_Mamalade((int)ItemPrivateNum.MAMALADE);
        Set_Orange_Potion((int)ItemPrivateNum.ORANGE_POTION);
        Set_Purple_Potion((int)ItemPrivateNum.PURPLE_POTION);
        Set_Bright_RedPotion((int)ItemPrivateNum.BRIGHT_REDPOTION);

        
        InstanTreasuerBox(pos, prinum);
    }
    private void Update()
    {
        ReloadScene();
    }

    private void ReloadScene()
    {
        
        if (Input.GetKeyDown("r"))
        {//reload scene, for testing purposes

            DestroyComponent();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    private void OnApplicationQuit()
    {
        DestroyComponent();
    }

    private void DestroyComponent()
    {
        Item temp;
        for (int i = 0; i < GetItemGam.Length; i++)
        {
            temp = GetItemGam[i].GetComponent<Item>();
            if (temp != null)
            {
                DestroyImmediate(temp, true);
            }

        }
    }

    public Item SearchingItem(int craft, int abs)
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].OutCraftNum().Equals(craft) && ItemList[i].OutAbsoluteNum().Equals(abs))
            {
                return ItemList[i];
            }
        }

        return null;
    }

    //보물상자 드랍(정보 넘기기) rate = 상자등급
    public void InstanTreasuerBox(Transform pos,ItemPrivateNum []num)
    {
        GameObject[] temp = new GameObject[num.Length];
        for(int i = 0; i < num.Length; i++)
        {
            temp[i] = GetItemGam[(int)num[i]];
        }
        GetBox = Instantiate(GetBoxGam[(int)BOXTYPE.TREASUREBOX], pos.position, Quaternion.identity).GetComponent<Treasure_Box>();
        GetBox.GetInfo(temp, GetItemAniGam, num.Length);
    }

    public void InstanEndRoomBox(Transform pos, List<int> num, Transform parent)
    {
        GameObject[] temp = new GameObject[num.Count];
        for (int i = 0; i < num.Count; i++)
        {
            temp[i] = GetItemGam[num[i]];
        }
        GetEndRoomBox = Instantiate(GetBoxGam[(int)BOXTYPE.ENDROOM], pos.position, Quaternion.identity).GetComponent<EndRoomTreasure>();
        GetEndRoomBox.transform.parent = parent;
        GetEndRoomBox.GetEndRoomInfo(temp, GetItemAniGam, num.Count);
    }

    //플레이어가 아이템과 충돌할 시(아이템을 먹을경우)
    public bool GetItemInfo(int num)
    {
        return inven.AddItem(ItemList[num]);
    }

    public List<int> SetRewardItem()
    {
        List<int> EndRoomReward = new List<int>();
        int randnum = Random.Range(2, 6);
        int randitem = 0;
        for (int i = 0; i < randnum; i++)
        {
            randitem = Random.Range(1, 101);
            if (randitem >= 1 && randitem < 6)
            {
                EndRoomReward.Add(11);
            }
            else if (randitem >= 6 && randitem < 15)
            {
                EndRoomReward.Add(Random.Range(9,11));
            }
            else if (randitem >= 15 && randitem < 50)
            {
                EndRoomReward.Add(Random.Range(5,9));
            }
            else if (randitem >= 50 && randitem < 100)
            {
                EndRoomReward.Add(Random.Range(1,5));
            }
        }
        return EndRoomReward;
    }



    /// /////////////////////////////////////////////////////////////////////////
    private void Set_Bigger_Potion(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Bigger_Potion>());
        InitItem(num, ItemType.CONSUME,"비거물약", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 134, 0);
    }

    private void Set_Wood(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Wood>());
        InitItem(num, ItemType.MATERIAL, "목재", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Stone(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Stone>());
        InitItem(num, ItemType.MATERIAL, "돌", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Flint(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Flint>());
        InitItem(num, ItemType.MATERIAL, "부싯돌", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Vine(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Vine>());
        InitItem(num, ItemType.MATERIAL, "넝쿨", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Seed(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Seed>());
        InitItem(num, ItemType.MATERIAL, "씨앗", WEAPONRATING.RARE, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Scrap_Metal(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Scrap_Metal>());
        InitItem(num, ItemType.MATERIAL, "고철", WEAPONRATING.RARE, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_GunPowder(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<GunPowder>());
        InitItem(num, ItemType.MATERIAL, "화약", WEAPONRATING.RARE, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Electric_wire(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Electric_Wire>());
        InitItem(num, ItemType.MATERIAL, "전선", WEAPONRATING.RARE, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Steel(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Steel>());
        InitItem(num, ItemType.MATERIAL, "강철", WEAPONRATING.UNIQUE, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Gold(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Gold>());
        InitItem(num, ItemType.MATERIAL, "금", WEAPONRATING.UNIQUE, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Meteorite_Debris(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Meteorite_Debris>());
        InitItem(num, ItemType.MATERIAL, "운석파편", WEAPONRATING.LEGEND, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Ruby(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Ruby>());
        InitItem(num, ItemType.MATERIAL, "루비", WEAPONRATING.UNKNOWN, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Sapphire(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Sapphire>());
        InitItem(num, ItemType.MATERIAL, "사파이어", WEAPONRATING.UNKNOWN, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Emerald(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Emerald>());
        InitItem(num, ItemType.MATERIAL, "에메랄드", WEAPONRATING.UNKNOWN, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Amethyst(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Amethyst>());
        InitItem(num, ItemType.MATERIAL, "자수정", WEAPONRATING.UNKNOWN, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Amber(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Amber>());
        InitItem(num, ItemType.MATERIAL, "엠버", WEAPONRATING.UNKNOWN, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Topaz(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Topaz>());
        InitItem(num, ItemType.MATERIAL, "토파즈", WEAPONRATING.UNKNOWN, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }


    /// /////////////////////////////////////////////////////////////////////////


    private void Set_Tooth_Pick(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Tooth_Pick>());//new 대신 addcomponent써야 노란색경고가 안나오더라 ㅋ;
        InitItem(num, ItemType.MELEE_WEAPON, "이쑤시개", WEAPONRATING.NORMAL, 0f, 0, 20, 0.3f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 2, 0);
    }
    private void Set_Club(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Club>());
        InitItem(num, ItemType.MELEE_WEAPON, "클럽", WEAPONRATING.NORMAL, 2f, 10, 30, 0.5f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 3, 1);
    }
    private void Set_Stone_Spear(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Stone_Spear>());
        InitItem(num, ItemType.MELEE_WEAPON, "돌창", WEAPONRATING.NORMAL, 2f, 10, 40, 0.7f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 4, 2);
    }
    private void Set_Vine_Whipping(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Vine_Whipping>());
        InitItem(num, ItemType.MELEE_WEAPON, "넝쿨채찍", WEAPONRATING.NORMAL, 2f, 10, 5, 0.5f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 8, 8);
    }
    private void Set_Wood_Shield(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Wood_Shield>());
        InitItem(num, ItemType.MELEE_WEAPON, "나무방패", WEAPONRATING.NORMAL, 2f, 10, 30, 0.5f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 4, 4);
    }
    private void Set_Spiral_Sword(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Spiral_Sword>());
        InitItem(num, ItemType.MELEE_WEAPON, "나선검", WEAPONRATING.RARE, 2f, 10, 65, 0.6f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 13, 11);
    }
    private void Set_RSowrd_LShield(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<RSowrd_LShield>());
        InitItem(num, ItemType.MELEE_WEAPON, "오른손엔검_왼손엔방패", WEAPONRATING.RARE, 2f, 10, 60, 0.5f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 41, 3);
    }
    private void Set_Kola(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Kola>());
        InitItem(num, ItemType.MELEE_WEAPON, "Kola", WEAPONRATING.RARE, 2f, 10, 65, 0.4f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 19, 17);
    }
    private void Set_Lapeer(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Lapeer>());
        InitItem(num, ItemType.MELEE_WEAPON, "레이피어", WEAPONRATING.RARE, 2f, 10, 35, 0.2f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 20, 18);
    }
    private void Set_Fork(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Fork>());
        InitItem(num, ItemType.MELEE_WEAPON, "포크", WEAPONRATING.UNIQUE, 2f, 10, 80, 0.3f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 36, 24);
    }
    private void Set_Sealed_Key(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Sealed_Key>());
        InitItem(num, ItemType.MELEE_WEAPON, "봉인된열쇠", WEAPONRATING.UNIQUE, 2f, 10, 100, 0.4f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 47, 29);
    }
    private void Set_Master_Key(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Master_Key>());
        InitItem(num, ItemType.MELEE_WEAPON, "마스터키", WEAPONRATING.LEGEND, 2f, 10, 130, 0.4f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 59, 3);
    }
    private void Set_Gauntlet(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Gauntlet>());
        InitItem(num, ItemType.MELEE_WEAPON, "???건틀릿", WEAPONRATING.UNKNOWN, 2f, 10, 60, 0.5f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 51, 39);
    }
    private void Set_Small_Key(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Small_Key>());
        InitItem(num, ItemType.MELEE_WEAPON, "???작은열쇠", WEAPONRATING.UNKNOWN, 2f, 10, 60, 0.5f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 53, 41);
    }
    private void Set_Wood_SlingShot(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Wood_SlingShot>());
        InitItem(num, ItemType.RANGED_WEAPON, "나무새총", WEAPONRATING.NORMAL, 2f, 20, 20, 0.5f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 3, 2);
    }
    private void Set_Wood_Bow(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Wood_Bow>());
        InitItem(num, ItemType.RANGED_WEAPON, "나무활", WEAPONRATING.NORMAL, 0.2f, 1, 35, 0.6f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 6, 4);
    }
    private void Set_Poison_Needle(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Poison_Needle>());
        InitItem(num, ItemType.RANGED_WEAPON, "독침", WEAPONRATING.NORMAL, 2f, 15, 15, 0.3f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 8, 6);
    }
    private void Set_Boomerang(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Boomerang>());
        InitItem(num, ItemType.RANGED_WEAPON, "부메랑", WEAPONRATING.NORMAL, 2f, 10, 5, 0.5f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 5, 3);
    }
    private void Set_Seeding(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Seeding>());
        InitItem(num, ItemType.RANGED_WEAPON, "씨뿌리기", WEAPONRATING.NORMAL, 2f, 10, 5, 0.5f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 9, 7);
    }
    private void Set_FireBird(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<FireBird>());
        InitItem(num, ItemType.RANGED_WEAPON, "불새", WEAPONRATING.RARE, 0.2f, 1, 40, 0.6f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 36, 30);
    }
    private void Set_HwaseungGun(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<HwaseungGun>());
        InitItem(num, ItemType.RANGED_WEAPON, "화승총", WEAPONRATING.RARE, 4f, 5, 70, 0.7f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 12, 10);
    }
    private void Set_Chakram(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Chakram>());
        InitItem(num, ItemType.RANGED_WEAPON, "차크람", WEAPONRATING.RARE, 0.5f, 5, 50, 0.5f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 16, 12);
    }
    private void Set_Nail_Gun(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Nail_Gun>());
        InitItem(num, ItemType.RANGED_WEAPON, "네일건", WEAPONRATING.RARE, 2f, 30, 30, 0.2f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 26, 14);
    }
    private void Set_Shield_Pistol(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Shield_Pistol>());
        InitItem(num, ItemType.RANGED_WEAPON, "쉴드피스톨", WEAPONRATING.UNIQUE, 2.5f, 20, 60, 0.6f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 37, 25);
    }
    private void Set_GentleBrella(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<GentleBrella>());
        InitItem(num, ItemType.RANGED_WEAPON, "신사우산", WEAPONRATING.UNIQUE, 2f, 25, 80, 0.5f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 34, 28);
    }
    private void Set_TFF_Gun(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<TFF_Gun>());
        InitItem(num, ItemType.RANGED_WEAPON, "369", WEAPONRATING.UNIQUE, 3f, 36, 63, 0.3f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 45, 41);
    }
    private void Set_Flax_Gun(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Flax_Gun>());
        InitItem(num, ItemType.RANGED_WEAPON, "Flax_Gun", WEAPONRATING.UNIQUE, 2f, 25, 80, 0.6f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 36, 11);
    }
    private void Set_EndOfCentury_Gauntlet(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<EndOfCentury_Gauntlet>());
        InitItem(num, ItemType.RANGED_WEAPON, "세기말의 건틀릿", WEAPONRATING.LEGEND, 2f, 10, 100, 0.6f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 117, 57);
    }
    private void Set_PineCone_Bomb(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<PineCone_Bomb>());
        InitItem(num, ItemType.RANGED_WEAPON, "솔방울 폭탄", WEAPONRATING.NORMAL, 2f, 10, 5, 0.5f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 5, 1);
    }
    private void Set_Seed_Bomb(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Seed_Bomb>());
        InitItem(num, ItemType.RANGED_WEAPON, "씨앗폭탄", WEAPONRATING.NORMAL, 2f, 10, 5, 0.5f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 8, 2);
    }
    private void Set_Robot_Toy(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Robot_Toy>());
        InitItem(num, ItemType.RANGED_WEAPON, "장난감로봇", WEAPONRATING.RARE, 2f, 10, 5, 0.5f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 23, 17);
    }
    private void Set_Plasma_Grenade(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Plasma_Grenade>());
        InitItem(num, ItemType.RANGED_WEAPON, "프라즈마 수류탄", WEAPONRATING.UNIQUE, 2f, 10, 5, 0.5f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 29, 19);
    }
    private void Set_Catapult(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Catapult>());
        InitItem(num, ItemType.RANGED_WEAPON, "투석기", WEAPONRATING.NORMAL, 0f, 10000, 40, 1.0f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 7, 5);
    }
    private void Set_SunFlower(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<SunFlower>());
        InitItem(num, ItemType.RANGED_WEAPON, "해바라기", WEAPONRATING.NORMAL, 0f, 10000, 50, 0.8f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 15, 11);
    }
    private void Set_TV(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<TV>());
        InitItem(num, ItemType.RANGED_WEAPON, "TV", WEAPONRATING.RARE, 0f, 10000, 60, 0.6f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 28, 16);
    }
    private void Set_Stone_Harubang(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Stone_Harubang>());
        InitItem(num, ItemType.RANGED_WEAPON, "돌하르방", WEAPONRATING.RARE, 0f, 10000, 80, 0.6f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 111, 107);
    }
    private void Set_MiniGun_Turret(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<MiniGun_Turret>());
        InitItem(num, ItemType.RANGED_WEAPON, "미니건포탑", WEAPONRATING.UNIQUE, 0f, 10000, 50, 0.2f, 1f, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 61, 47);
    }
    /// /////////////////////////////////////////////////////////////////////////

    private void Set_Apple(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Apple>());
        InitItem(num, ItemType.CONSUME, "사과", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Banana(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Banana>());
        InitItem(num, ItemType.CONSUME, "바나나", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Carrot(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Carrot>());
        InitItem(num, ItemType.CONSUME, "당근", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Strawberry(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Strawberry>());
        InitItem(num, ItemType.CONSUME, "딸기", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Orange(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Orange>());
        InitItem(num, ItemType.CONSUME, "오렌지", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Blueberry(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Blueberry>());
        InitItem(num, ItemType.CONSUME, "블루베리", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Green_Apple(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Green_Apple>());
        InitItem(num, ItemType.CONSUME, "풋사과", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Mango(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Mango>());
        InitItem(num, ItemType.CONSUME, "망고", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }
    private void Set_Dragon_Fruit(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Dragon_Fruit>());
        InitItem(num, ItemType.CONSUME, "용과", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite);
    }


    /// /////////////////////////////////////////////////////////////////////////

    private void Set_Wood_Wall(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Wood_Wall>());
        InitItem(num, ItemType.CONSUME, "나무벽", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 1, 1);
    }
    private void Set_Stone_Wall(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Stone_Wall>());
        InitItem(num, ItemType.CONSUME, "돌벽", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 70, 66);
    }
    private void Set_Rugged_IronWall(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Rugged_IronWall>());
        InitItem(num, ItemType.CONSUME, "투박한 철벽", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 75, 63);
    }
    private void Set_Iron_Wall(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Iron_Wall>());
        InitItem(num, ItemType.CONSUME, "철벽", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 79, 61);
    }
    private void Set_CarrotApple_Juice(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<CarrotApple_Juice>());
        InitItem(num, ItemType.CONSUME, "당근사과주스", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 112, 2);
    }
    private void Set_BlueberryMango_Smoothie(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<BlueberryMango_Smoothie>());
        InitItem(num, ItemType.CONSUME, "블루베리망고스무디", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 122, 2);
    }
    private void Set_Doubleberry(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Doubleberry>());
        InitItem(num, ItemType.CONSUME, "더블베리", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 118, 2);
    }
    private void Set_StrawberryBanana_Shake(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<StrawberryBanana_Shake>());
        InitItem(num, ItemType.CONSUME, "딸기바나나쉐이크", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 114, 2);
    }
    private void Set_Sorry_Potion(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Sorry_Potion>());
        InitItem(num, ItemType.CONSUME, "죄송합니다", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 116, 118);
    }
    private void Set_Mamalade(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Mamalade>());
        InitItem(num, ItemType.CONSUME, "머말에이드", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 118, 0);
    }
    private void Set_Red_Potion(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Red_Potion>());
        InitItem(num, ItemType.CONSUME, "빨간물약", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 110, 0);
    }
    private void Set_Yellow_Potion(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Yellow_Potion>());
        InitItem(num, ItemType.CONSUME, "노란물약", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 118, 6);
    }
    private void Set_Blue_Potion(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Blue_Potion>());
        InitItem(num, ItemType.CONSUME, "파란물약", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 116, 4);
    }
    private void Set_Giant_Potion(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Giant_Potion>());
        InitItem(num, ItemType.CONSUME, "거대화물약", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 120, 2);
    }
    private void Set_Orange_Potion(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Orange_Potion>());
        InitItem(num, ItemType.CONSUME, "주황물약", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 129, 1);
    }
    private void Set_Purple_Potion(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Purple_Potion>());
        InitItem(num, ItemType.CONSUME, "보라물약", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 131, 1);
    }
    private void Set_Bright_RedPotion(int num)
    {
        ItemList.Add(GetItemGam[num].AddComponent<Bright_RedPotion>());
        InitItem(num, ItemType.CONSUME, "새빨간물약", WEAPONRATING.NORMAL, GetItemGam[num].GetComponent<SpriteRenderer>().sprite, 119, 9);
    }


    //조합 무기 아이템(조합번호와 절대값번호포함)
    void InitItem(int privatenum,ItemType itemtype, string name, WEAPONRATING rating, float reload, int bullet, int dmg, float attspeed, float range, Sprite icon, int craftnum, int absnum)
    {
        ItemList[privatenum].GetPrivateNum(privatenum);
        ItemList[privatenum].i_itemtype = itemtype;
        ItemList[privatenum].GetCraftNum(craftnum);
        ItemList[privatenum].GetAbsNum(absnum);
        ItemList[privatenum].s_name = name;
        ItemList[privatenum].i_rating = rating;

        ItemList[privatenum].i_dmg = dmg;
        ItemList[privatenum].f_attspeed = attspeed;
        ItemList[privatenum].f_range = range;
        ItemList[privatenum].i_FullBulletAmount = bullet;
        ItemList[privatenum].f_ReloadTime = reload;


        ItemList[privatenum].S_Icon = icon;
    }

    //조합 사용 아이템
    void InitItem(int privatenum, ItemType itemtype, string name, WEAPONRATING rating, Sprite icon, int craftnum, int absnum)
    {
        ItemList[privatenum].GetPrivateNum(privatenum);
        ItemList[privatenum].i_itemtype = itemtype;
        ItemList[privatenum].GetCraftNum(craftnum);
        ItemList[privatenum].GetAbsNum(absnum);
        ItemList[privatenum].s_name = name;
        ItemList[privatenum].i_rating = rating;

        ItemList[privatenum].i_dmg = 0;
        ItemList[privatenum].f_attspeed = 0;
        ItemList[privatenum].f_range = 0;


        ItemList[privatenum].S_Icon = icon;
    }
    //조합아이템이 아닌 기본 아이템
    void InitItem(int privatenum, ItemType itemtype, string name, WEAPONRATING rating, Sprite icon)
    {
        ItemList[privatenum].GetPrivateNum(privatenum);
        ItemList[privatenum].i_itemtype = itemtype;
        ItemList[privatenum].s_name = name;
        ItemList[privatenum].i_rating = rating;

        ItemList[privatenum].i_dmg = 0;
        ItemList[privatenum].f_attspeed = 0;
        ItemList[privatenum].f_range = 0;


        ItemList[privatenum].S_Icon = icon;
    }

}