using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string s_name { get; set; }
    public int i_dmg { get; set; }
    public WEAPONRATING i_rating { get; set; }
    public float f_attspeed { get; set; }
    public float f_range { get; set; }
    public Sprite S_Icon { get; set; }


    protected abstract void DestroyItem();
    protected virtual void CollPlayer(Collider2D coll,int num)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            GameObject gam = GameObject.Find("ItemMng");
            if (gam.GetComponent<ItemMng>().GetItemInfo(num))
            {
                DestroyItem();
            }
            
        }
    }
}
