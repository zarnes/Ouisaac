using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretBonus : MonoBehaviour
{

    public Sprite bonusSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (Player.Instance == null)
            return;
        if (collision.attachedRigidbody.gameObject != Player.Instance.gameObject)
            return;


        Player.Instance.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = bonusSprite;
        Player.Instance.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;

        Destroy(gameObject);
    }
}
