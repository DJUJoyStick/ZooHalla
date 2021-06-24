using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : Item
{
    new string name;
    int dmg;
    WEAPONRATING rating;
    float attspeed;
    float range;
    Sprite Icon;

    [SerializeField]
    //������ �ĺ���ȣ
    int num = 1;

    private void Start()
    {
        direction = ItemOutInitial();
    }

    void Update()
    {
        if (!b_CollCheck)
        {
            ItemOut(direction);
        }      
    }

    public string s_name
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
    public int i_dmg
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
    public WEAPONRATING i_rating
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
    public float f_attspeed
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
    public float f_range
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
    public Sprite S_Icon
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

    public void OnCollisionEnter2D(Collision2D coll)
    {
        CollPlayer(coll, num);
    }

    protected override void DestroyItem()
    {
        Destroy(transform.parent.gameObject);
    }

}
