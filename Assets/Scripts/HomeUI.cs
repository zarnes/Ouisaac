using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeUI : MonoBehaviour
{
    public InputField SeedText;

    // Start is called before the first frame update
    void Start()
    {
        SeedText.text = Random.Range(1000, 9999).ToString();
    }

    public void StartGame()
    {
        int seed;
        if (int.TryParse(SeedText.text, out seed))
        {
            GameManager.Instance.Seed = seed;
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
