using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingTrap : MonoBehaviour
{

    public GameObject arrow;
    public GameObject spawnPosition;

    public enum MoveState
    {
        UP,
        DOWN,
        RIGHT,
        LEFT,
    }
    public MoveState moveS = MoveState.UP;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("shot", 2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void shot()
    {
        if (moveS == MoveState.UP)
        {
            arrow.GetComponent<movingArrow>().moveX = 0;
            arrow.GetComponent<movingArrow>().moveY = 10;
        }else if(moveS == MoveState.DOWN)
        {
            arrow.GetComponent<movingArrow>().moveY = -10;
            arrow.GetComponent<movingArrow>().moveX = 0;
        }
        else if(moveS == MoveState.LEFT)
        {
            arrow.GetComponent<movingArrow>().moveX = -10;
            arrow.GetComponent<movingArrow>().moveY = 0;
        }
        else if (moveS == MoveState.RIGHT)
        {
            arrow.GetComponent<movingArrow>().moveY = 0;
            arrow.GetComponent<movingArrow>().moveX = 10;
        }
        Instantiate(arrow, spawnPosition.transform.position, spawnPosition.transform.rotation);
        
        StartCoroutine("WaitBeforeShoot");
    }

    public IEnumerator WaitBeforeShoot()
    {
        yield return new WaitForSeconds(2f);
        shot();
    }
}
