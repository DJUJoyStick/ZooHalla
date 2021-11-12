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
    private int gutter = 10;

    // Start is called before the first frame update
    void Start()
    {
        Initializing();
    }


    private void Initializing()
    {
        MapPlayerTrans = Instantiate(GetMapPlayerSpr, Vector2.zero, Quaternion.identity).transform;
        MapPlayerTrans.parent = GetMiniMapRoot.transform;
    }

    public void GetMapPlayerMove(Collision2D GetTag)
    {
        Vector2 tempVec2 = Vector2.zero;
        if (GetTag.gameObject.CompareTag("UpDoor"))
        {
            tempVec2.y = map_y;
        }
        else if (GetTag.gameObject.CompareTag("DownDoor"))
        {
            tempVec2.y = -map_y;
        }
        else if (GetTag.gameObject.CompareTag("LeftDoor"))
        {
            tempVec2.x = -map_x;
        }
        else if (GetTag.gameObject.CompareTag("RightDoor"))
        {
            tempVec2.x = map_x;
        }
        Render_MiniMap_Sprite(tempVec2);
    }
    public Vector2 GetPlayerMove(Collision2D GetTag)
    {

        Vector2 tempVec2 = Vector2.zero;
        if (GetTag.gameObject.CompareTag("UpDoor"))
        {
            tempVec2.y = gutter;
            NextStageStopPlayer(SGameMng.I.PlayerType);
        }
        else if (GetTag.gameObject.CompareTag("DownDoor"))
        {
            tempVec2.y = -gutter;
            NextStageStopPlayer(SGameMng.I.PlayerType);
        }
        else if (GetTag.gameObject.CompareTag("LeftDoor"))
        {
            tempVec2.x = -gutter;
            NextStageStopPlayer(SGameMng.I.PlayerType);
        }
        else if (GetTag.gameObject.CompareTag("RightDoor"))
        {
            tempVec2.x = gutter;
            NextStageStopPlayer(SGameMng.I.PlayerType);

            
        }
        return tempVec2;

        //GetTrans.Translate(tempVec2);

    }

    void NextStageStopPlayer(PLAYERTYPE type)
    {
        StartCoroutine(SGameMng.I.PlayerSc.DoorToNextStage());
    }

    private void Render_MiniMap_Sprite(Vector2 Vec2)
    {
        MapPlayerTrans.transform.Translate(Vec2);
    }
}