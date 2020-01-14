using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Ouisaac
{
    public class RoomsManager : MonoBehaviour
    {
        public List<RoomPrefabs> Rooms;

        public RoomPrefabs Find(System.Random rnd, bool top = false, bool right = false, bool down = false, bool left = false, bool needKey = false)
        {
            List<RoomPrefabs> possibleRooms = new List<RoomPrefabs>();
            foreach(RoomPrefabs room in Rooms)
            {
                if (top && room.TopDoor == RoomPrefabs.DoorPossibility.Never)
                    continue;
                if (!top && room.TopDoor == RoomPrefabs.DoorPossibility.Always)
                    continue;

                if (right && room.RightDoor == RoomPrefabs.DoorPossibility.Never)
                    continue;
                if (!right && room.RightDoor == RoomPrefabs.DoorPossibility.Always)
                    continue;

                if (down && room.DownDoor == RoomPrefabs.DoorPossibility.Never)
                    continue;
                if (!down && room.DownDoor == RoomPrefabs.DoorPossibility.Always)
                    continue;

                if (left && room.LeftDoor == RoomPrefabs.DoorPossibility.Never)
                    continue;
                if (!left && room.LeftDoor == RoomPrefabs.DoorPossibility.Always)
                    continue;

                if (room.hasKey != needKey)
                    continue;

                possibleRooms.Add(room);
            }

            if (possibleRooms.Count == 0)
                return null;

            return WeightedRandom(possibleRooms, rnd);

            /*int rand = rnd.Next(possibleRooms.Count);
            return possibleRooms[rand];*/
        }

        private RoomPrefabs WeightedRandom(List<RoomPrefabs> rooms, System.Random rnd)
        {
            int maxWeight = rooms.Select(r => r.Weight).Sum(); ;
            int randomWeight = rnd.Next(maxWeight);

            foreach(RoomPrefabs room in rooms)
            {
                if (randomWeight <= room.Weight)
                    return room;

                randomWeight -= room.Weight;
            }

            Debug.LogError("Waighted random failed");
            return null;
        }
    }

    [System.Serializable]
    public class RoomPrefabs
    {
        public GameObject Prefab;
        public int Weight = 100;
        public DoorPossibility TopDoor;
        public DoorPossibility RightDoor;
        public DoorPossibility DownDoor;
        public DoorPossibility LeftDoor;
        public bool hasKey;

        public enum DoorPossibility
        {
            Possible,
            Always,
            Never,
        }
    }
}
