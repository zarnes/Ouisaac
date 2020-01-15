using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indice : MonoBehaviour
{
    public void Setup(Direction direction)
    {
        Transform h = transform.Find("Horizontal");
        Transform v = transform.Find("Vertical");

        switch (direction)
        {
            case Direction.None:
                h.gameObject.SetActive(false);
                v.gameObject.SetActive(false);
                break;
            case Direction.Horizontal:
                h.gameObject.SetActive(false);
                v.gameObject.SetActive(true);
                break;
            case Direction.Vertical:
                h.gameObject.SetActive(true);
                v.gameObject.SetActive(false);
                break;
        }
    }

    public enum Direction
    {
        None,
        Horizontal,
        Vertical
    }
}
