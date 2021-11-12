using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemType i_itemtype;
    public string s_name;
    public int i_dmg;
    public int i_FullBulletAmount;
    public float f_ReloadTime;
    public WEAPONRATING i_rating;
    public float f_attspeed;
    public float f_range;
    public Sprite S_Icon;

    public bool b_CollCheck = false;

    protected Vector2 direction; // 아이템이 튕겨져나오는 위치벡터

    protected abstract void DestroyItem();

    public abstract void GetPrivateNum(int num);
    public virtual void GetCraftNum(int num) { }
    public virtual void GetAbsNum(int num) { }

    public abstract int OutPrivateNum();
    public abstract int OutCraftNum();
    public abstract int OutAbsoluteNum();

    protected virtual void CollPlayer(Collision2D coll, int num)
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


    protected virtual Vector2 ItemOutInitial()
    {
        float randnum = Random.Range(-5f, 5f);
        Vector2 RandomVec = new Vector2(transform.localPosition.x + randnum, transform.localPosition.y - 1.5f);
        return RandomVec;
    }
    protected virtual void ItemOut(Vector2 desVec2, float speed = 3f)
    {
        
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, desVec2, speed*Time.deltaTime);
    }

    public void ItemColl()//아이템이 나오고 몇초뒤에 먹게해주는 함수(애니메이션트리거사용)
    {
        //이거 생각해보니 콜라이더 건들면 안됨 나중에 bool로 수정하자
        GetComponent<CircleCollider2D>().enabled = true;
    }

    
    
}
