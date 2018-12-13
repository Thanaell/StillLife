using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopOnLastDay : MonoBehaviour {

    float timer;
    public float timeBeforeMute;
	// Use this for initialization
	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (DayManager.Instance.currentDay.dayNumber == 6)
        {
            timer += Time.deltaTime;
           // Debug.Log(timer);
            if(timer > timeBeforeMute)
            {
                GetComponent<AudioSource>().mute = true;
            }
        }
	}
}
