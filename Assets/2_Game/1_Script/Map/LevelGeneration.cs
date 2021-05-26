using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapState
{
    Normal = 0,
    Start,
    Boss,
    Event
}

public class LevelGeneration : MonoBehaviour
{
    Vector2 worldSize = new Vector2(3, 3);
    Room[,] rooms;
    [SerializeField]
    List<Vector2> takenPositions = new List<Vector2>();
    int gridSizeX, gridSizeY;
    [SerializeField]
    private int numberOfRooms;//방갯수조정은 이 변수로 조정할 것
    private int minroom;
    private int maxroom;
    private int eventnum;
    private int[] randnumarr;
    private int[] randnum;

    public GameObject roomWhiteObj;//미니맵 스프라이트
    public Transform mapRoot;//처음 시작맵 위치
    void Start()
    {
        Initial();
        CreateRooms(); //실제 지도를 작성하다.
        SetRoomDoors(); //객실이 연결되는 도어를 지정합니다.
        DrawMap(); //지도를 만들기 위해 객체를 인스턴스화하다.
        GetComponent<SheetAssigner>().Assign(rooms); //수준 지오메트리 생성을 처리하는 다른 스크립트로 룸 정보를 전달합니다.
    }

    private void Initial()
    {
        minroom = 7;
        maxroom = 11;
        numberOfRooms = Random.Range(minroom, maxroom);
        

        if (numberOfRooms < 9)
        {
            eventnum = 1;
            randnum = new int[eventnum];
            randnum[0] = Random.Range(1, numberOfRooms - 2);
        }
        else
        {
            eventnum = 2;
            randnum = new int[eventnum];
            randnum[0] = Random.Range(1, numberOfRooms - 2);
            do
            {
                randnum[1] = Random.Range(1, maxroom - 2);
            }
            while (randnum[0] == randnum[1]);

            //swap
            if (randnum[0] > randnum[1])
            {
                int temp = randnum[0];
                randnum[0] = randnum[1];
                randnum[1] = temp;
            }
        }



        if (numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2))
        { // 방의 갯수가 (x(4)*2) * (y(4)*2) = 64 64개를 초과한다면
            numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
        }

        gridSizeX = Mathf.RoundToInt(worldSize.x); //note: these are half-extents
        gridSizeY = Mathf.RoundToInt(worldSize.y);//매개변수가 float로 받고 반올림혹은 반내림하여 정수로 바꿔준다 0.5는 짝수로 반환한다
    }

    void CreateRooms()
    {
        //setup
        rooms = new Room[gridSizeX * 2, gridSizeY * 2];
        rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, (int)MapState.Start);
        takenPositions.Insert(0, Vector2.zero);
        Vector2 checkPos = Vector2.zero;
        //magic numbers
        float randomCompare, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;
        int index = 0;
        //add rooms
        for (int i = 1; i < numberOfRooms; i++)
        {
            float randomPerc = ((float)i) / (((float)numberOfRooms - 1));

            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
            //grab new position
            checkPos = NewPosition();

            //test new position
            if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare)
            {
                int iterations = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    iterations++;
                } while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
                if (iterations >= 50)
                    print("error: could not create with fewer neighbors than : " + NumberOfNeighbors(checkPos, takenPositions));
            }

            //방의 정보를 생성자를 통하여 기입
            if (i == numberOfRooms - 1)//마지막 방 보스로 지정
            {
                rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, (int)MapState.Boss);
            }

            else if (index < eventnum && i == randnum[index])//랜덤 위치에 이벤트방 생성
            {
                rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, (int)MapState.Event);
                index++;
            }
            else//그 이외는 노말맵
            {
                rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, (int)MapState.Normal);
            }

            takenPositions.Add(checkPos);//기존엔 insert(0,checkPos) 이었다. 그렇게 하면 계속 0번에 값이 들어가므로 첫번째 0,0은 마지막으로 가게된다
                                         //rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY].pos_x = 
        }
    }

    Vector2 NewPosition()
    {
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1)); // pick a random room
            x = (int)takenPositions[index].x;//그 x, y 위치를 포착하다.
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);//수평 또는 수직 축을 보기 위해 임의로 선택합니다.
            bool positive = (Random.value < 0.5f);//그 축에 참 또는 거짓이 될 것인지 고르다.
            if (UpDown)
            { //find the position bnased on the above bools
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY); //make sure the position is valid
        return checkingPos;
    }
    Vector2 SelectiveNewPosition()
    { // 방법은 두 가지 논평을 통해 위와 다르다.
        int index = 0, inc = 0;
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            inc = 0;
            do
            {
                //비어있는 형용사를 찾기위해 방을 얻는 대신에, 우리는 오직 하나만으로 시작한다.
                //한 이웃으로서 이렇게 하면 분기된 객실을 반환할 가능성이 높아집니다.
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                inc++;
            } while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100);
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (UpDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        if (inc >= 100)
        { // 너무 오래 걸리는 경우 차단 루프: 이 루프는 솔루션을 찾는 데 도움이 되지 않습니다. 이 루프에는 문제가 없습니다.
            print("Error: could not find position with only one neighbor");
        }
        return checkingPos;
    }
    int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions)
    {
        int ret = 0; // 0에서 시작하여 각 면에 1을 더하면 이미 방이 있다.
        if (usedPositions.Contains(checkingPos + Vector2.right))
        { //using Vector.[direction] as short hands, for simplicity
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.left))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.up))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.down))
        {
            ret++;
        }
        return ret;
    }
    void DrawMap()
    {
        foreach (Room room in rooms)
        {
            if (room == null)
            {
                continue; //skip where there is no room
            }
            Vector2 drawPos = room.gridPos;
            drawPos.x *= 16;//aspect ratio of map sprite
            drawPos.y *= 8;
            //맵 객체를 만들고 해당 변수를 할당합니다.
            MapSpriteSelector mapper = Object.Instantiate(roomWhiteObj, drawPos, Quaternion.identity).GetComponent<MapSpriteSelector>();
            mapper.type = room.type;
            mapper.up = room.doorTop;
            mapper.down = room.doorBot;
            mapper.right = room.doorRight;
            mapper.left = room.doorLeft;
            mapper.gameObject.transform.parent = mapRoot;
        }
    }
    void SetRoomDoors()
    {
        for (int x = 0; x < ((gridSizeX * 2)); x++)
        {
            for (int y = 0; y < ((gridSizeY * 2)); y++)
            {
                if (rooms[x, y] == null)
                {
                    continue;
                }
                Vector2 gridPosition = new Vector2(x, y);
                if (y - 1 < 0)
                { //check above
                    rooms[x, y].doorBot = false;
                }
                else
                {
                    rooms[x, y].doorBot = (rooms[x, y - 1] != null);
                }
                if (y + 1 >= gridSizeY * 2)
                { //check bellow
                    rooms[x, y].doorTop = false;
                }
                else
                {
                    rooms[x, y].doorTop = (rooms[x, y + 1] != null);
                }
                if (x - 1 < 0)
                { //check left
                    rooms[x, y].doorLeft = false;
                }
                else
                {
                    rooms[x, y].doorLeft = (rooms[x - 1, y] != null);
                }
                if (x + 1 >= gridSizeX * 2)
                { //check right
                    rooms[x, y].doorRight = false;
                }
                else
                {
                    rooms[x, y].doorRight = (rooms[x + 1, y] != null);
                }
            }
        }
    }
}
