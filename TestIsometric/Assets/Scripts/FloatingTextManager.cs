using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManager : Singleton<FloatingTextManager> {

    protected FloatingTextManager () { }

    public float displayTime = 2f;

    private TextMesh textMesh;

	// Use this for initialization
	void Start () {
        textMesh = GetComponent<TextMesh>();		
	}
	
	// Update is called once per frame
	void Update () {		
	}

    public void DisplayText(string text) 
    {
        textMesh.text = text;
    }

    public void HideText()
    {
        textMesh.text = "";
    }

    public IEnumerator DisplayHideText(string text)
    {
        textMesh.text = text;
        yield return new WaitForSecondsRealtime(displayTime);
        textMesh.text = "";
    }

}
