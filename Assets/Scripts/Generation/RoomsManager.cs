using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ouisaac
{
    public class RoomsManager : MonoBehaviour
    {
        public List<Room> Rooms;

        public Room Find(System.Random rnd, bool top = false, bool right = false, bool down = false, bool left = false)
        {
            List<Room> possibleRooms = new List<Room>();
            foreach(Room room in Rooms)
            {
                if (top && room.TopDoor == Room.DoorPossibility.Never)
                    continue;
                if (!top && room.TopDoor == Room.DoorPossibility.Always)
                    continue;

                if (right && room.RightDoor == Room.DoorPossibility.Never)
                    continue;
                if (!right && room.RightDoor == Room.DoorPossibility.Always)
                    continue;

                if (down && room.DownDoor == Room.DoorPossibility.Never)
                    continue;
                if (!down && room.DownDoor == Room.DoorPossibility.Always)
                    continue;

                if (left && room.LeftDoor == Room.DoorPossibility.Never)
                    continue;
                if (!left && room.LeftDoor == Room.DoorPossibility.Always)
                    continue;

                possibleRooms.Add(room);
            }

            if (possibleRooms.Count == 0)
                return null;

            int rand = rnd.Next(possibleRooms.Count);
            return possibleRooms[rand];
        }
    }

    [System.Serializable]
    public class Room
    {
        public GameObject Prefab;
        public DoorPossibility TopDoor;
        public DoorPossibility RightDoor;
        public DoorPossibility DownDoor;
        public DoorPossibility LeftDoor;

        public enum DoorPossibility
        {
            Possible,
            Always,
            Never,
        }
    }
}
