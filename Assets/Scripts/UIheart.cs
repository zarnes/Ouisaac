using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class UIheart : MonoBehaviour
{
    public Image[] hearts;
    public GameObject deathPanel;

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

    public void YouDied()
    {
        Time.timeScale = 0;
        deathPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
