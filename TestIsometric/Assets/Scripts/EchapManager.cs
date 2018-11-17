using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EchapManager : MonoBehaviour {
    public GameObject menuCanvas;
    public GameObject creditsImage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuCanvas.activeSelf)
            {
                menuCanvas.SetActive(false);
                creditsImage.SetActive(false);
            }
            else{
                menuCanvas.SetActive(true);
                }
        }
	}

    public void OnMenuButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void OnCreditsButtonPressed()
    {
        creditsImage.SetActive(true);
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
