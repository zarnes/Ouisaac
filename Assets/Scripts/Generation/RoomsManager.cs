using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Ouisaac
{
    public class RoomsManager : MonoBehaviour
    {
        public List<RoomPrefabs> Rooms;

        public RoomPrefabs Find(System.Random rnd, bool top = false, bool right = false, bool down = false, bool left = false, bool needKey = false, bool needHint = false, bool isStart = false, bool isEnd = false, bool isSecret = false, int difficulty = 1)
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

                if (isSecret != room.IsSecretRoom)
                    continue;
                
                possibleRooms.Add(room);
            }

            if (possibleRooms.Count == 0)
                return null;

            return WeightedRandom(possibleRooms, rnd, difficulty);

            /*int rand = rnd.Next(possibleRooms.Count);
            return possibleRooms[rand];*/
        }

        private RoomPrefabs WeightedRandom(List<RoomPrefabs> rooms, System.Random rnd, int targetDifficulty)
        {
            int maxWeight = 0;
            foreach (RoomPrefabs room in rooms)
            {
                int diffChange = Mathf.Abs(room.Difficulty - targetDifficulty);
                float diffModifier;
                if (diffChange != 0)
                    diffModifier = 1f / diffChange;
                else
                    diffModifier = 2;
                int weight = (int) (room.Weight * diffModifier);

                maxWeight += weight;
            }
            //int maxWeight = rooms.Select(r => r.Weight).Sum();
            int randomWeight = rnd.Next(maxWeight);

            foreach(RoomPrefabs room in rooms)
            {
                /*if (randomWeight <= room.Weight)
                    return room;

                randomWeight -= room.Weight;*/

                int diffChange = Mathf.Abs(room.Difficulty - targetDifficulty);
                float diffModifier;
                if (diffChange != 0)
                    diffModifier = 1f / diffChange;
                else
                    diffModifier = 2;
                int weight = (int)(room.Weight * diffModifier);

                if (randomWeight <= weight)
                    return room;

                randomWeight -= weight;
            }

            Debug.LogError("Weighted random failed");
            return null;
        }
    }

    [System.Serializable]
    public class RoomPrefabs
    {
        public GameObject Prefab;
        public int Weight = 100;
        public int Difficulty = 1;
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
