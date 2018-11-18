using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManager : Singleton<FloatingTextManager> {

    protected FloatingTextManager () { }

    public float displayTime = 4f;

    private TextMesh actions;
    public TextMesh hint;

	// Use this for initialization
	void Start () {
        actions = GetComponent<TextMesh>();		
	}
	
	// Update is called once per frame
	void Update () {		
	}

    public void DisplayText(string text) 
    {
        actions.text = text;
    }

    public void HideText()
    {
        actions.text = "";
    }

    public IEnumerator DisplayHideText(string text)
    {
        hint.text = text;
        yield return new WaitForSecondsRealtime(displayTime);
        hint.text = "";
    }

    public IEnumerator DisplayHideText(string text, float time)
    {
        hint.text = text;
        yield return new WaitForSecondsRealtime(time);
        hint.text = "";
    }

}
