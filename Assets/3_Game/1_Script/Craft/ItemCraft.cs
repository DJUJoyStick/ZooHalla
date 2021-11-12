using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ItemCraft : MonoBehaviour
{

    public Tuple<int, int> FindItem(List<int> arr)
    {
        arr.Sort();
        int count = arr.Count;
        int craftnum = 0;
        int absnum = 0;
        int expressionnum = 0;
        //절대값이 -1(리스트에빠진값)이 아닐때 까지 루프
        for (int i = 0; i < count; i++)
        {
            if (!arr[i].Equals(-1))
            {
                absnum = arr[i];
                expressionnum = i;
                break;

            }
        }

        for (int i = expressionnum; i < count; i++)
        {
            craftnum += arr[i];
        }
        for (int i = expressionnum+1; i < count; i++)
        {
            absnum -= arr[i];
        }
        absnum = Mathf.Abs(absnum);
        return new Tuple<int, int>(craftnum, absnum);
    }

}
