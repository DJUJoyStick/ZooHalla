using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfCentury_Gauntlet : Item
{
    //static으로 해야 매개변수로 전달할때 지역변수로아닌
    //힙 메모리에 올라와있는 변수로 넘기기에 정보 공유가 가능하다
    private static int privatenum;
    private static int craftnum;
    private static int absolutenum;
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
    public override void GetCraftNum(int num)
    {
        craftnum = num;
    }
    public override void GetAbsNum(int num)
    {
        absolutenum = num;
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
        return craftnum;
    }

    public override int OutAbsoluteNum()
    {
        return absolutenum;
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
