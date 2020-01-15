using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ouisaac
{
    public class RoomCreator : MonoBehaviour
    {
        public void CreateRoom(RoomPrefabs room, Node node, Vector2 position)
        {
            GameObject roomGo = Instantiate(room.Prefab, new Vector3(position.x, position.y), Quaternion.identity);

            Room roomScript = roomGo.GetComponent<Room>();
            roomScript.position.x = node.X;
            roomScript.position.y = node.Y;
            roomScript.isStartRoom = node.Start;

            roomGo.GetComponentInChildren<Indice>()?.Setup(node.indice);

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
                
                door.SetState(node.doors[directionIdx]);

            }
        }
    }
}