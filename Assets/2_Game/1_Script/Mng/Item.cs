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

    public bool b_CollCheck = false;

    protected Vector2 direction; // �������� ƨ���������� ��ġ����

    protected abstract void DestroyItem();

    protected virtual void CollPlayer(Collision2D coll,int num)
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

    public void ItemColl()//�������� ������ ���ʵڿ� �԰����ִ� �Լ�(�ִϸ��̼�Ʈ���Ż��)
    {
        //�̰� �����غ��� �ݶ��̴� �ǵ�� �ȵ� ���߿� bool�� ��������
        GetComponent<CircleCollider2D>().enabled = true;
    }
    
}
