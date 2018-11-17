using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: Singleton<GameManager> {

    public GameObject creditsImage;
    protected GameManager() { }

    private void Update()
    {
      if (creditsImage.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                creditsImage.SetActive(false);
            }
        }
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadCredits()
    {
        creditsImage.SetActive(true);
    }
}
