using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingArrow : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveX = 0;
    public float moveY = 0;

    void Start()
    {
        Invoke("waitBeforeDestroy",1f);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(new Vector3(moveX, moveY, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Player.Instance == null)
            return;
        if (collision.attachedRigidbody.gameObject != Player.Instance.gameObject)
            return;

        if (Player.Instance.life > 0)
        {
            Player.Instance.ApplyHit(null);

        }

        Destroy(transform.gameObject);
    }

    void waitBeforeDestroy()
    {
        Destroy(transform.gameObject);
    }
}
