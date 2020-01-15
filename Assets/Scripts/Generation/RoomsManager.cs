using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Ouisaac
{
    public class RoomsManager : MonoBehaviour
    {
        public List<RoomPrefabs> Rooms;

        public RoomPrefabs Find(System.Random rnd, bool top = false, bool right = false, bool down = false, bool left = false, bool needKey = false, bool needHint = false, bool isStart = false, bool isEnd = false, bool isSecret = false)
        {
            List<RoomPrefabs> possibleRooms = new List<RoomPrefabs>();
            foreach(RoomPrefabs room in Rooms)
            {
                if (top && room.TopDoor == RoomPrefabs.Possibility.Never)
                    continue;
                if (!top && room.TopDoor == RoomPrefabs.Possibility.Always)
                    continue;

                if (right && room.RightDoor == RoomPrefabs.Possibility.Never)
                    continue;
                if (!right && room.RightDoor == RoomPrefabs.Possibility.Always)
                    continue;

                if (down && room.DownDoor == RoomPrefabs.Possibility.Never)
                    continue;
                if (!down && room.DownDoor == RoomPrefabs.Possibility.Always)
                    continue;

                if (left && room.LeftDoor == RoomPrefabs.Possibility.Never)
                    continue;
                if (!left && room.LeftDoor == RoomPrefabs.Possibility.Always)
                    continue;

                if (room.hasKey != needKey)
                    continue;

                if (isStart && !room.CanBeStart)
                    continue;

                if (isEnd && !room.CanBeEnd)
                    continue;

                if (needHint && !room.ContainHint)
                    continue;

                if (isSecret && !room.IsSecretRoom)
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
        public Possibility TopDoor;
        public Possibility RightDoor;
        public Possibility DownDoor;
        public Possibility LeftDoor;
        public bool hasKey;
        public bool CanBeStart;
        public bool CanBeEnd;
        public bool ContainHint;
        public bool IsSecretRoom;

        public enum Possibility
        {
            Possible,
            Always,
            Never,
        }
    }
}
