using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private UIheart updateUI;

    private void Awake()
    {
        updateUI = GameObject.Find("Canvas").GetComponent<UIheart>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (Player.Instance == null)
            return;
        if (collision.attachedRigidbody.gameObject != Player.Instance.gameObject)
            return;

        if(Player.Instance.life < 5)
        {
            Player.Instance.life += 1;
            updateUI.EarnUIheart();
            Destroy(transform.gameObject);
        }
        
    }
}
