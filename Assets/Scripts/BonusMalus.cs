using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BonusMalus : MonoBehaviour
{
    /*public Sprite fasterAttack;
    public Sprite slowerAttack;
    public Sprite speedUp;
    public Sprite speedDown;
    public Sprite lifeTouch;
    public Sprite deathTouch;

    public Image itemImage;*/

    private enum ITEM
    {
        FasterAttack,
        SlowerAttack,
        SpeedUp,
        SpeedDown,
        LifeTouch,
        DeathTouch
    }

    private void RestoreParameters()
    {
        Player.Instance.attackCooldown = 0.3f;
        Player.Instance.defaultMovement.speedMax = 2.0f;
    }

    private void RandomItem()
    {
        List<int> odds = new List<int>();
        List<ITEM> items = new List<ITEM>();

        items.Add(ITEM.FasterAttack);
        odds.Add(15);
        items.Add(ITEM.SlowerAttack);
        odds.Add(30);
        items.Add(ITEM.SpeedUp);
        odds.Add(57);
        items.Add(ITEM.SpeedDown);
        odds.Add(84);
        items.Add(ITEM.LifeTouch);
        odds.Add(92);
        items.Add(ITEM.DeathTouch);
        odds.Add(100);

        System.Random rnd = new System.Random();
        int randomDrawn = rnd.Next(100);
        Debug.Log("Draw : " + randomDrawn);

        ITEM theItem = items.SkipWhile((item, index) => randomDrawn >= odds[index]).First();
        Debug.Log("Item : " + theItem);

        ItemEffect(theItem);
    }

    private void ItemEffect(ITEM item)
    {
        switch (item)
        {
            case ITEM.FasterAttack:
                Player.Instance.attackCooldown = 0.15f;
                break;
            case ITEM.SlowerAttack:
                Player.Instance.attackCooldown = 0.45f;
                break;
            case ITEM.SpeedUp:
                Player.Instance.defaultMovement.speedMax = 4.0f;
                break;
            case ITEM.SpeedDown:
                Player.Instance.defaultMovement.speedMax = 1.0f;
                break;
            case ITEM.LifeTouch:
                Player.Instance.life += 1;
                break;
            case ITEM.DeathTouch:
                Player.Instance.ApplyHit(null);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RestoreParameters();
            RandomItem();
        }
    }
}
