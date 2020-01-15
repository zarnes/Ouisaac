using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIheart : MonoBehaviour
{
    public Image[] hearts;

    public void EarnUIheart()
    {
        Image theHeart = hearts.Where(heart => heart.enabled == false).First();
        theHeart.enabled = true;
    }

    public void LoseUIheart()
    {
        Image theHeart = hearts.Where(heart => heart.enabled == true).Last();
        theHeart.enabled = false;
    }

}
