using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : Item
{
    //static���� �ؾ� �Ű������� �����Ҷ� ���������ξƴ�
    //�� �޸𸮿� �ö���ִ� ������ �ѱ�⿡ ���� ������ �����ϴ�
    private static int privatenum;
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
    public override void GetPrivateNum(int num)
    {
        privatenum = num;
    }

    public override int OutPrivateNum()
    {
        return privatenum;
    }
    public override int OutCraftNum()
    {
        return -1;
    }

    public override int OutAbsoluteNum()
    {
        return -1;
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {

        CollPlayer(coll, privatenum);
    }

    protected override void DestroyItem()
    {
        Destroy(transform.parent.gameObject);
    }
}
