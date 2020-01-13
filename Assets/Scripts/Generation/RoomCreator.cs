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

            Door[] doors = roomGo.GetComponentsInChildren<Door>();

            foreach (Door door in doors)
            {
                door.SetOrientation();
                int directionIdx = -1;
                switch (door.Orientation)
                {
                    case Utils.ORIENTATION.NORTH:
                        directionIdx = 0;
                        break;
                    case Utils.ORIENTATION.EAST:
                        directionIdx = 1;
                        break;
                    case Utils.ORIENTATION.SOUTH:
                        directionIdx = 2;
                        break;
                    case Utils.ORIENTATION.WEST:
                        directionIdx = 3;
                        break;
                }

                //bool open = node.directions[directionIdx];
                /*bool open = node.doors[directionIdx] != Door.STATE.CLOSED;
                if (open)
                    door.SetState(Door.STATE.OPEN);
                else
                    door.SetState(Door.STATE.WALL);*/

                door.SetState(node.doors[directionIdx]);

            }
        }
    }
}