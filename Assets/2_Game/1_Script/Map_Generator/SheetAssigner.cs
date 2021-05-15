using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetAssigner : MonoBehaviour {
	[SerializeField]
	Texture2D[] sheetsNormal;
	[SerializeField]
	GameObject RoomObj;
    [SerializeField]
    GameObject RoomParent;
    private int room_num = 0;
    //List<Vector2> GetPos;
	public Vector2 roomDimensions = new Vector2(17,9);//272,144(16에서 4로 축소 밑에도)68,36
	public Vector2 gutterSize = new Vector2(4, 4);//144,64 36,16
	public void Assign(Room[,] rooms){
		foreach (Room room in rooms){
			//skip point where there is no room
			if (room == null){
				continue;
			}
            //pick a random index for the array
            //int index = Mathf.RoundToInt(Random.value * (sheetsNormal.Length -1));
            //find position to place room
			Vector3 pos = new Vector3(room.gridPos.x * (roomDimensions.x + gutterSize.x), room.gridPos.y * (roomDimensions.y + gutterSize.y), 0);
            RoomInstance myRoom = Instantiate(RoomObj, pos, Quaternion.identity).GetComponent<RoomInstance>();
			myRoom.Setup(room.gridPos, room.type, room.doorTop, room.doorBot, room.doorLeft, room.doorRight, room_num++);
			myRoom.transform.parent = RoomParent.transform;
        }
    }

}
