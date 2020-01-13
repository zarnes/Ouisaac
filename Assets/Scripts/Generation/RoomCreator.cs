using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ouisaac
{
    public class RoomCreator : MonoBehaviour
    {
        public /*GameObject*/ void CreateRoom(Room room, Node node, Vector2 position)
        {
            GameObject roomGo = Instantiate(room.Prefab, new Vector3(position.x, position.y), Quaternion.identity);

            Door[] doors = roomGo.GetComponentsInChildren<Door>();

            foreach (Door door in doors)
            {
                //int directionIdx = -1; TODO set door orientation before that
                int directionIdx = 0;
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

                bool open = node.directions[directionIdx];
                /*if (open)
                    door.SetState(Door.STATE.OPEN);
                else
                    door.SetState(Door.STATE.CLOSED);*/

                door.SetState(Door.STATE.OPEN);

            }

            /*return roomGo;*/

        }
    }
}