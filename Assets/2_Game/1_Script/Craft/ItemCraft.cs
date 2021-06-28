using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemCraft : MonoBehaviour
{

    public Tuple<int,int> FindItem(List<int> arr)
    {
        arr.Sort();
        int count = arr.Count;
        int craftnum = 0;
        int absnum = arr[0];
        for(int i = 0; i < count; i++)
        {
            craftnum += arr[i];
        }
        for (int i = 1; i < count; i++)
        {
            absnum -= arr[i];
        }
        absnum = Mathf.Abs(absnum);
        return new Tuple<int, int>(craftnum, absnum);
    }
    
}
