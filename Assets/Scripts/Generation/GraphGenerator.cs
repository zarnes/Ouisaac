using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGenerator : MonoBehaviour
{
    public Vector2 Scale;
    public int Seed;
    public int Rooms = 10;
    public List<Node> Nodes = new List<Node>();

    private System.Random rnd;

    // Start is called before the first frame update
    void Start()
    {
        rnd = new System.Random(Seed);
        Node first = new Node();
        first.X = first.Y = 0;
        Nodes.Add(first);

        CreatePath(first, Rooms - 1);
        //CreatePath(Nodes[5], 3);
        CreatePath(Nodes[2], 5);
    }

    void CreatePath(Node start, int length)
    {
        Node parent = start;

        int i = length;
        while (i > 0)
        {
            if (i != length)
                parent = Nodes[Nodes.Count - 1];

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
                        --i;
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
    }

    Node SpawnNode(Node parent, Direction direction)
    {
        int X = parent.X;
        int Y = parent.Y;

        switch (direction)
        {
            case Direction.up:
                --Y;
                break;
            case Direction.right:
                ++X;
                break;
            case Direction.down:
                ++Y;
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
                parent.directions[0] = newNode.directions[0] = true;
                break;
            case Direction.right:
                parent.directions[1] = newNode.directions[1] = true;
                break;
            case Direction.down:
                parent.directions[2] = newNode.directions[2] = true;
                break;
            case Direction.left:
                parent.directions[3] = newNode.directions[3] = true;
                break;
        }

        return newNode;
    }

    private void OnDrawGizmosSelected()
    {
        foreach (Node node in Nodes)
        {
            Gizmos.color = Color.white;
            float X = node.X * Scale.x;
            float Y = node.Y * Scale.y;
            //Gizmos.DrawWireSphere(new Vector3(X, Y), .2f);
            Gizmos.DrawWireCube(new Vector3(X, Y), new Vector3(Scale.x * .9f, Scale.y * .9f));

            if (node.Parent != null)
            {
                float parentX = node.Parent.X * Scale.x;
                float parentY = node.Parent.Y * Scale.y;

                Gizmos.DrawLine(new Vector3(X, Y), new Vector3(parentX, parentY));
            }
        }
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
    public int X = 0;
    public int Y = 0;

    /*public bool up = false;
    public bool right = false;
    public bool down = false;
    public bool left = false;*/

    public bool[] directions = new bool[4];

    //public int pUp, pRight, pDown, pLeft = 25;
    public int[] weights = new int[4];
    public Node Parent = null;

    public Node()
    {
        for (int i = 0; i < 4; ++i)
        {
            directions[i] = false;
            weights[i] = 25;
        }
    }
}