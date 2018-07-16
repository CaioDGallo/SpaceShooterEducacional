using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private void Awake()
    {
        Debug.Log((PlayerPrefs.HasKey("Highscore")).ToString());
        Debug.Log((PlayerPrefs.GetInt("Highscore")).ToString());
        if (!PlayerPrefs.HasKey("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", 0);
            PlayerPrefs.SetInt("1", 0);
            PlayerPrefs.SetInt("2", 0);
            PlayerPrefs.SetInt("3", 0);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void Gabarito()
    {
        SceneManager.LoadScene("Gabarito");
    }

    public void Ranking()
    {
        SceneManager.LoadScene("Ranking");
    }
}
