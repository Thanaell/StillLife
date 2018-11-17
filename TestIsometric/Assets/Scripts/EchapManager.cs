using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchapManager : MonoBehaviour {
    public GameObject menuCanvas;
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
            }
            else{
                menuCanvas.SetActive(true);
                }
        }
	}
}
