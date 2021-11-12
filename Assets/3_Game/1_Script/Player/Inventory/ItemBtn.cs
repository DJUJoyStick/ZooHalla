using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class ItemBtn : MonoBehaviour
{
    public int nSlotNum;
    private RectTransform ItemTrans;
    private int nItemNum;
    //private Item GetItemInfo;
    public GameObject GetItemInfoGam;
    public Text GetItemInfoText;
    private Inventory GetInven;
    public GameObject GetCraftSelectImg;
    private Player PlayerSc;
    private Item GetItemInfo;
    private int nItemPrivateNum;
    private int nListNum;
    private bool bSelectWeapon;

    private void Start()
    {
        ItemTrans = GetComponent<RectTransform>();
        GetInven = SGameMng.I.GetInven;
        PlayerSc = SGameMng.I.PlayerSc;
        bSelectWeapon = false;
    }
    private void Update()
    {
        ItemDestroy();
        NoReciepeItem();
    }
    public void SelectedItem()
    {
        transform.position = Input.mousePosition;
        GetComponent<BoxCollider2D>().enabled = false;

    }

    public void GetInfo(Item item, int nSlot)
    {
        GetItemInfo = item;
        nItemNum = item.OutPrivateNum();
        nItemPrivateNum = item.OutPrivateNum();
        //번째 수 기입
        nSlotNum = nSlot;
        GetItemInfoText.color = SetRating(item.i_rating);
        if (item.i_itemtype.Equals(ItemType.MATERIAL) || item.i_itemtype.Equals(ItemType.CONSUME))
        {
            GetItemInfoText.text =
                item.s_name
                + "\n\n"
                + item.i_rating
                + " 등급 \n\n";
        }
        else
        {
            GetItemInfoText.text =
                item.s_name
                + "\n\n"
                + item.i_rating
                + " 등급 \n\n"
                + item.i_dmg
                + " 데미지 \n\n"
                + "공격속도: "
                + item.f_attspeed
                + "\n\n";
        }


    }

    private Color SetRating(WEAPONRATING rating)
    {
        if (rating.Equals(WEAPONRATING.RARE))
        {
            return Color.blue;
        }
        else if (rating.Equals(WEAPONRATING.UNIQUE))
        {
            return Color.magenta;
        }
        else if (rating.Equals(WEAPONRATING.LEGEND))
        {
            return Color.red;
        }
        else if (rating.Equals(WEAPONRATING.UNKNOWN))
        {
            return Color.grey;
        }
        else
        {
            return Color.white;
        }

    }
    //제작이 성공됐을때 재료들은 소멸되는 함수
    public void ItemDestroy()
    {
        if (GetCraftSelectImg.activeInHierarchy.Equals(true) && GetInven.bisCrafting)
        {
            GetInven.SetSlot(nSlotNum, false);
            GetInven.SlotCollOn(nSlotNum);
            
            if (GetInven.tempcount++ >= GetInven.CraftItems.Count-1)
            {
                GetInven.tempcount = 0;
                GetInven.bisCrafting = false;
                GetInven.CraftItems.Clear();
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
    //제작이 실패되었을때 선택된 아이템들을 선택해제시켜주는 함수
    public void NoReciepeItem()
    {
        if (GetInven.bisReciepe)
        {
            if(GetInven.tempcount++ >= GetInven.CraftItems.Count - 1)
            {
                GetCraftSelectImg.SetActive(false);
                GetInven.bisReciepe = false;
                GetInven.tempcount = 0;
               GetInven.CraftItems.Clear();
            }
            else
            {
                GetCraftSelectImg.SetActive(false);
            }

        }
       
    }

    //이 아이템버튼을 눌렸을경우
    public void Craft_Selected()
    {
        if (GetInven.bCraftMode.Equals(true))
        {
            if (GetCraftSelectImg.activeInHierarchy.Equals(false))
            {
                GetCraftSelectImg.SetActive(true);
                nListNum = GetInven.CraftItems.Count;
                GetInven.CraftItems.Add(nItemNum);
            }
            else
            {
                GetCraftSelectImg.SetActive(false);
               // RearrangeList(ref GetInven.CraftItems, nListNum);
                GetInven.CraftItems.RemoveAt(nListNum);
                GetInven.CraftItems.Insert(nListNum, -1);
                GetInven.tempcount++;
            }

        }
        else
        {
            GetCraftSelectImg.SetActive(false);
        }
    }

    public void UseItem()
    {
        if ((int)GetItemInfo.OutPrivateNum() >= 18 && (int)GetItemInfo.OutPrivateNum() <= 54)
        {
            PlayerSc._PlayerWeaponSr.enabled = true;
            SGameMng.I.TextMngSc.NowWeaponText.text = GetItemInfo.s_name;
            PlayerSc._PlayerWeaponSr.sprite = GetItemInfo.S_Icon;
            PlayerSc._nFullBulletAmount = GetItemInfo.i_FullBulletAmount;
            PlayerSc._nBulletAmount = GetItemInfo.i_FullBulletAmount;
            PlayerSc._nWeaponDmg = GetItemInfo.i_dmg;
            PlayerSc._PlayerWeapon = (ItemPrivateNum)GetItemInfo.OutPrivateNum();
            PlayerSc._animatorOverrideController["Weapon_Attack"] = PlayerSc._PlayerWeaponAttackAniClip[GetItemInfo.OutPrivateNum() - 18];
            PlayerSc._fAttackSpeed = GetItemInfo.f_attspeed;
            PlayerSc._fReloadTime = GetItemInfo.f_ReloadTime;
            PlayerSc._PlayerWeaponType = GetItemInfo.i_itemtype;
            SGameMng.I.TextMngSc.AmountText.text = "Amount : " + SGameMng.I.PlayerSc._nFullBulletAmount + " / " + SGameMng.I.PlayerSc._nFullBulletAmount;
            SGameMng.I.b_WeaponSelected = true;
        }
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

                nSlotNum = GetNumInString(temp.name);//전의 슬롯숫자를 바뀐 슬롯숫자로 바꿔줌

                GetInven.SetSlot(nSlotNum, true);
                GetInven.SlotCollOff(nSlotNum);
            }
        }
        else
        {
            

                SGameMng.I.PlayerSc._PlayerWeaponSr.enabled = false;
                SGameMng.I.TextMngSc.NowWeaponText.text = "Hand";
                SGameMng.I.TextMngSc.AmountText.text = "Amount : " + " 0/0 ";
                SGameMng.I.b_WeaponSelected = false;
                bSelectWeapon = false;
            
            var Item = Instantiate(GetInven.GetItemmng.GetItemAniGam, Vector2.zero, Quaternion.identity);
            Instantiate(GetInven.GetItemmng.GetItemGam[nItemPrivateNum], SGameMng.I.PlayerSc.transform.position, Quaternion.identity, Item.transform);
            GetInven.SetSlot(nSlotNum, false);
            Destroy(gameObject);
        }

        ItemTrans.anchoredPosition = Vector2.zero;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    //문자열에서 숫자만 추출하여 반환
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
