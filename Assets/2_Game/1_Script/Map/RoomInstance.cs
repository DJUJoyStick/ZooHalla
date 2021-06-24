using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class RoomInstance : MonoBehaviour
{
    [HideInInspector]
    public Vector2 gridPos;
    private int type; // 0: 보통맵, 1: 시작맵, 2:보스맵 3:이벤트맵
    private string MapName; //맵 이름
    private static int height = 9;
    private static int width = 17;
    [HideInInspector]
    public bool doorTop, doorBot, doorLeft, doorRight;
    [SerializeField]
    GameObject doorU, doorD, doorL, doorR, doorWall, doorWallColl;
    public TilemapRenderer GetMapTileRend;//최적화용
    public Transform GetWalls;//최적화용 
    private const float tileSize = 1;//타일 크기 건들지 말것
    static int mapsize = 2;
    Vector2 roomSizeInTiles = new Vector2(height * mapsize, width * mapsize);//9,17 가로세로 길이
    private Transform DoorInfoTrans;

    public void Setup(Vector2 _gridPos, int _type, bool _doorTop, bool _doorBot, bool _doorLeft, bool _doorRight, string str)
    {
        gridPos = _gridPos;
        type = _type;
        doorTop = _doorTop;
        doorBot = _doorBot;
        doorLeft = _doorLeft;
        doorRight = _doorRight;
        MapName = str;
        GenerateRoomTiles();


    }

    void PlaceDoor(Vector3 spawnPos, bool door, GameObject doorSpawn, int door_pos)
    {
        // check whether its a door or wall, then spawn
        if (door)
        {
            DoorInfoTrans = Instantiate(doorSpawn, spawnPos, Quaternion.identity).transform;
            DoorInfoTrans.transform.parent = transform;
            if (door_pos == (int)DOORPOS.TOP)
            {
                DoorInfoTrans.tag = "UpDoor";
            }
            else if (door_pos == (int)DOORPOS.DOWN)
            {
                DoorInfoTrans.tag = "DownDoor";
            }
            else if (door_pos == (int)DOORPOS.LEFT)
            {
                DoorInfoTrans.tag = "LeftDoor";
            }
            else if (door_pos == (int)DOORPOS.RIGHT)
            {
                DoorInfoTrans.tag = "RightDoor";
            }
        }
        else
        {
            Instantiate(doorWallColl, spawnPos, Quaternion.identity).transform.parent = GetWalls;
        }
    }


    void GenerateRoomTiles()
    {
        Vector3 spawnPos;
        //loop through every pixel of the texture
        for (int x = 0; x < width * mapsize; x++)
        {
            spawnPos = positionFromTileGrid(x, 0);
            if (x == width * mapsize / 2)
            {
                PlaceDoor(spawnPos, doorTop, doorU, (int)DOORPOS.TOP);
            }
            else
            {
                Instantiate(doorWall, spawnPos, Quaternion.identity).transform.parent = GetWalls;
            }

            spawnPos = positionFromTileGrid(x, 17);
            if (x == width * mapsize / 2)
            {
                PlaceDoor(spawnPos, doorBot, doorD, (int)DOORPOS.DOWN);
            }
            else
            {
                Instantiate(doorWall, spawnPos, Quaternion.identity).transform.parent = GetWalls;
            }

        }
        for (int y = 0; y < height * mapsize; y++)
        {
            spawnPos = positionFromTileGrid(0, y);
            if (y == height * mapsize / 2)
            {
                PlaceDoor(spawnPos, doorLeft, doorL, (int)DOORPOS.LEFT);
            }
            else
            {
                Instantiate(doorWall, spawnPos, Quaternion.identity).transform.parent = GetWalls;
            }

            spawnPos = positionFromTileGrid(33, y);
            if (y == height * mapsize / 2)
            {
                PlaceDoor(spawnPos, doorRight, doorR, (int)DOORPOS.RIGHT);
            }
            else
            {
                Instantiate(doorWall, spawnPos, Quaternion.identity).transform.parent = GetWalls;
            }

        }
        GetWalls.gameObject.SetActive(false);
    }
    //void GenerateTile(int x, int y)
    //{//픽셀 색깔에 맞게 타일이 생성되는 코드
    //    Color pixelColor = tex.GetPixel(x, y);
    //    //skip clear spaces in texture
    //    if (pixelColor.a == 0)
    //    {
    //        return;
    //    }
    //    //find the color to math the pixel
    //    foreach (ColorToGameObject mapping in mappings)
    //    {
    //        if (mapping.color.Equals(pixelColor))
    //        {
    //            Vector3 spawnPos = positionFromTileGrid(x, y);
    //            Instantiate(mapping.prefab, spawnPos, Quaternion.identity).transform.parent = this.transform;
    //        }
    //        else
    //        {
    //            //forgot to remove the old print for the tutorial lol so I'll leave it here too
    //            //print(mapping.color + ", " + pixelColor);
    //        }
    //    }
    //}
    Vector3 positionFromTileGrid(int x, int y)
    {
        const float x_offset = 0.5f;
        const float y_offset = 0.25f;
        Vector3 ret;
        //find difference between the corner of the texture and the center of this object
        Vector3 offset = new Vector3((-roomSizeInTiles.x + 1) * tileSize + x_offset, (roomSizeInTiles.y / 4) * tileSize - (tileSize / 4) + y_offset, 0);
        //find scaled up position at the offset
        ret = new Vector3(tileSize * (float)x, -tileSize * (float)y, 0) + offset + transform.position;
        return ret;
    }

    //플레이어가 해당맵에 들어온것을 감지
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            Debug.Log(MapName + "입장");
            GetMapTileRend.enabled = true;
            GetWalls.gameObject.SetActive(true);
        }
    }

    //플레이어가 해당맵에 나간것을 감지
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            Debug.Log(MapName + "퇴장");
            GetMapTileRend.enabled = false;
            GetWalls.gameObject.SetActive(false);
        }
    }


}