using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooth_Pick : Item
{
    new string name;
    int dmg;
    int rating;
    float attspeed;
    float range;
    Sprite Icon;

    [SerializeField]
    int num = 0;
    public new string s_name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }

    }

    public new int i_dmg
    {
        get
        {
            return dmg;
        }
        set
        {
            dmg = value;
        }
    }

    public new int i_rating
    {
        get
        {
            return rating;
        }
        set
        {
            rating = value;
        }
    }

    public new float f_attspeed
    {
        get
        {
            return attspeed;
        }
        set
        {
            attspeed = value;
        }
    }

    public new float f_range
    {
        get
        {
            return range;
        }
        set
        {
            range = value;
        }
    }

    public new Sprite S_Icon
    {
        get
        {
            return Icon;
        }
        set
        {
            Icon = value;
        }

    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            GameObject gam = GameObject.Find("ItemMng");
            gam.GetComponent<ItemMng>().GetItemInfo(num);
            DestroyItem();
        }
    }

    protected override void DestroyItem()
    {
        Destroy(gameObject);
    }



    //public Tooth_Pick(string name, int num, int rating, int dmg, float speed, float range)
    //{
    //    this.name = name;
    //    this.num = num;
    //    this.rating = rating;
    //    this.dmg = dmg;
    //    this.attspeed = speed;
    //    this.range = range;
    //}



}
