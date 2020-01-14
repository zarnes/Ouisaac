using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ouisaac
{
    public class GraphGenerator : MonoBehaviour
    {
        public Vector2 Scale;
        public int Seed;
        private int oldSeed;
        public int Rooms = 10;
        public List<Node> Nodes = new List<Node>();

        private System.Random rnd;
        private RoomsManager roomsManager;
        private RoomCreator creator;

        [Range(0, 1)]
        public float probaDoor;
        private int doorClosed;

        // Start is called before the first frame update
        void Start()
        {
            roomsManager = GetComponent<RoomsManager>();
            creator = GetComponent<RoomCreator>();

            GenerateGraph(true);
        }

        private void Update()
        {
            if (oldSeed != Seed)
            {
                oldSeed = Seed;
                GenerateGraph(false);
            }
        }

        private void GenerateGraph(bool draw)
        {
            rnd = new System.Random(Seed);
            Nodes = new List<Node>();

            Node start = new Node();
            start.X = start.Y = 0;
            start.Start = true;
            Nodes.Add(start);

            CreatePath(start, Rooms - 1);
            Nodes[Nodes.Count - 1].End = true;

            CreateSecondaryPath();

            if (draw)
                DrawRooms();
        }

        int CreatePath(Node start, int length)
        {
            Node parent = start;

            int created = 0;
            while (created < length)
            {
                if (created > 0)
                //if (remaining != length)
                    parent = Nodes[Nodes.Count - 1];

                bool possible = false;
                for (int weightsI = 0; weightsI < 4; ++weightsI)
                {
                    if (parent.weights[weightsI] != 0)
                        possible = true;
                }
                if (!possible)
                {
                    //Debug.LogWarning("[SEED " + Seed + "] Can't finish path, missing " + remaining + " iterations");
                    Debug.LogWarning("[SEED " + Seed + "] Can't finish path,  (" + created + "/" + length + " iterations), blocked on [" + parent.X + ";" + parent.Y + "]");
                    //Rooms = i; TODO I may have broke something
                    return created;
                }

                int maxRand = 0;
                for (int randI = 0; randI < 4; ++randI)
                    maxRand += parent.weights[randI];

                int rand = rnd.Next(maxRand);
                Node newNode = null;

                for (int directionIdx = 0; directionIdx < 4; ++directionIdx)
                {
                    if (rand <= parent.weights[directionIdx])
                    {
                        newNode = SpawnNode(parent, (Direction)directionIdx);
                        if (newNode != null)
                        {
                            Nodes.Add(newNode);
                            //--remaining;
                            ++created;
                            break;
                        }
                        else
                        {
                            parent.weights[directionIdx] = 0;
                        }
                    }
                    rand -= parent.weights[directionIdx];
                }
            }

            return created;
        }

        Node SpawnNode(Node parent, Direction direction)
        {
            int X = parent.X;
            int Y = parent.Y;

            switch (direction)
            {
                case Direction.up:
                    ++Y;
                    break;
                case Direction.right:
                    ++X;
                    break;
                case Direction.down:
                    --Y;
                    break;
                case Direction.left:
                    --X;
                    break;
            }

            Node alreadyNode = Nodes.Find(n => n.X == X && n.Y == Y);
            if (alreadyNode != null)
                return null;

            Node newNode = new Node();
            newNode.X = X;
            newNode.Y = Y;
            newNode.Parent = parent;

            switch (direction)
            {
                case Direction.up:
                    parent.doors[0] = newNode.doors[2] = Door.STATE.OPEN;
                    break;
                case Direction.right:
                    parent.doors[1] = newNode.doors[3] = Door.STATE.OPEN;
                    break;
                case Direction.down:
                    parent.doors[2] = newNode.doors[0] = Door.STATE.OPEN;
                    break;
                case Direction.left:
                    parent.doors[3] = newNode.doors[1] = Door.STATE.OPEN;
                    break;
            }

            return newNode;
        }

        void DrawRooms()
        {
            for (int i = Nodes.Count - 1; i >= 0; --i)
            {
                Node node = Nodes[i];
                Room room = roomsManager.Find(
                    rnd,
                    node.doors[0] != Door.STATE.CLOSED,
                    node.doors[1] != Door.STATE.CLOSED,
                    node.doors[2] != Door.STATE.CLOSED,
                    node.doors[3] != Door.STATE.CLOSED
                );

                if (room != null)
                {
                    creator.CreateRoom(room, node, new Vector2(node.X * Scale.x, node.Y * Scale.y));
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            foreach (Node node in Nodes)
            {
                Gizmos.color = Color.white;
                float X = node.X * Scale.x;
                X += Scale.x / 2;
                float Y = node.Y * Scale.y;
                Y += Scale.y / 2;
                Gizmos.DrawWireCube(new Vector3(X, Y), new Vector3(Scale.x * .9f, Scale.y * .9f));

                if (node.Parent != null)
                {
                    float parentX = node.Parent.X * Scale.x;
                    parentX += Scale.x / 2;
                    float parentY = node.Parent.Y * Scale.y;
                    parentY += Scale.y / 2;
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(new Vector3(X, Y), new Vector3(parentX, parentY));
                }

                if (node.Start)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireSphere(new Vector3(X, Y), 2);
                }

                if (node.End)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(new Vector3(X, Y), 2);
                }

                if (node.ContainKey)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireSphere(new Vector3(X, Y), 2);
                }

                #region Closed doors drawing

                if (node.doors[0] == Door.STATE.CLOSED)
                {
                    Gizmos.color = Color.yellow;
                    Vector3 from = new Vector3(X - Scale.x / 4, Y + Scale.y / 2.1f);
                    Vector3 to = new Vector3(X + Scale.x / 4, Y + Scale.y / 2.1f);
                    Gizmos.DrawLine(from, to);
                }

                if (node.doors[1] == Door.STATE.CLOSED)
                {
                    Gizmos.color = Color.yellow;
                    Vector3 from = new Vector3(X + Scale.x / 2.1f, Y - Scale.y / 4);
                    Vector3 to = new Vector3(X + Scale.x / 2.1f, Y + Scale.y / 4);
                    Gizmos.DrawLine(from, to);
                }

                if (node.doors[2] == Door.STATE.CLOSED)
                {
                    Gizmos.color = Color.yellow;
                    Vector3 from = new Vector3(X - Scale.x / 4, Y - Scale.y / 2.1f);
                    Vector3 to = new Vector3(X + Scale.x / 4, Y - Scale.y / 2.1f);
                    Gizmos.DrawLine(from, to);
                }

                if (node.doors[3] == Door.STATE.CLOSED)
                {
                    Gizmos.color = Color.yellow;
                    Vector3 from = new Vector3(X - Scale.x / 2.1f, Y - Scale.y / 4);
                    Vector3 to = new Vector3(X - Scale.x / 2.1f, Y + Scale.y / 4);
                    Gizmos.DrawLine(from, to);
                }

                #endregion
            }
            if (doorClosed != 0)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(new Vector3((Nodes[doorClosed].X * Scale.x) + Scale.x/2, (Nodes[doorClosed].Y * Scale.y) + Scale.y /2), new Vector3(Scale.x * .9f, Scale.y * .9f));
            }
        }

        void CreateSecondaryPath()
        {
            int proba = (int)(Rooms * probaDoor);
            int tmpProba = rnd.Next(Rooms - proba - 2); /*Random.Range(proba, Rooms - proba);*/
            int created = CreatePath(Nodes[tmpProba], 5);
            while (created == 0)
            {
                --tmpProba;
                created = CreatePath(Nodes[tmpProba], 5);
            }

            //doorClosed = rnd.Next(Rooms - tmpProba - 1) + tmpProba;
            doorClosed = rnd.Next(tmpProba + 1, Rooms);
            Node doorNode = Nodes[doorClosed];

            if (doorNode.Y < doorNode.Parent.Y)
                doorNode.doors[0] = doorNode.Parent.doors[2] = Door.STATE.CLOSED;
            if (doorNode.Y > doorNode.Parent.Y)
                doorNode.doors[2] = doorNode.Parent.doors[0] = Door.STATE.CLOSED;
            if (doorNode.X < doorNode.Parent.X)
                doorNode.doors[1] = doorNode.Parent.doors[3] = Door.STATE.CLOSED;
            if (doorNode.X > doorNode.Parent.X)
                doorNode.doors[3] = doorNode.Parent.doors[1] = Door.STATE.CLOSED;

            Nodes[Nodes.Count - 1].ContainKey = true;
        }

        public enum Direction
        {
            up = 0,
            right = 1,
            down = 2,
            left = 3
        }
    }

    [System.Serializable]
    public class Node
    {
        public bool Start = false;
        public bool End = false;
        public bool ContainKey = false;

        public int X = 0;
        public int Y = 0;
        
        public Door.STATE[] doors = new Door.STATE[4];
        public int[] weights = new int[4];
        public Node Parent = null;

        public Node()
        {
            for (int i = 0; i < 4; ++i)
            {
                doors[i] = Door.STATE.WALL;
                weights[i] = 25;
            }
        }
    }
}
