using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ouisaac
{
    public class RoomCreator : MonoBehaviour
    {
        public void CreateRoom(Room room, Node node, Vector2 position)
        {
            GameObject roomGo = Instantiate(room.Prefab, new Vector3(position.x, position.y), Quaternion.identity);

            List<Transform> doorsTf = new List<Transform>();
            //doorsTf = roomGo.tag

            for (int i = 0; i < 4; ++i)
            {
                Door door = null;
                door.SetState(Door.STATE.CLOSED);
            }
        }
    }
}
