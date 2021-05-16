using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMapPosition : MonoBehaviour
{
    public GameObject GetMapPlayerSpr;
    public Transform GetMiniMapRoot;

    private Transform MapPlayerTrans;
    [SerializeField]
    private int map_x = 16;
    [SerializeField]
    private int map_y = 8;
    [SerializeField]
    private int gutter = 12;

    // Start is called before the first frame update
    void Start()
    {
        Initializing();
    }


    private void Initializing()
    {
        var level = GetComponent<LevelGeneration>();
        MapPlayerTrans = Instantiate(GetMapPlayerSpr, Vector2.zero, Quaternion.identity).transform;
        MapPlayerTrans.parent = GetMiniMapRoot.transform;
    }

    public void GetMapPlayerMove(Collider2D GetTag)
    {
        Vector2 tempVec2 = Vector2.zero;
        if (GetTag.CompareTag("UpDoor"))
        {
            tempVec2.y = map_y;
        }
        else if (GetTag.CompareTag("DownDoor"))
        {
            tempVec2.y = -map_y;
        }
        else if (GetTag.CompareTag("LeftDoor"))
        {
            tempVec2.x = -map_x;
        }
        else if (GetTag.CompareTag("RightDoor"))
        {
            tempVec2.x = map_x;
        }
        Render_MiniMap_Sprite(tempVec2);
    }
    public Vector2 GetPlayerMove(Collider2D GetTag)
    {
        Vector2 tempVec2 = Vector2.zero;
        if (GetTag.CompareTag("UpDoor"))
        {
            tempVec2.y = gutter;
        }
        else if (GetTag.CompareTag("DownDoor"))
        {
            tempVec2.y = -gutter;
        }
        else if (GetTag.CompareTag("LeftDoor"))
        {
            tempVec2.x = -gutter;
        }
        else if (GetTag.CompareTag("RightDoor"))
        {
            tempVec2.x = gutter;
        }
        //GetTrans.Translate(tempVec2);
        return tempVec2;
    }

    private void Render_MiniMap_Sprite(Vector2 Vec2)
    {
        MapPlayerTrans.transform.Translate(Vec2);
    }
}