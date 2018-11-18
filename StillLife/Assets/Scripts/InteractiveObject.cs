using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour {
    public DailyTaskType dailyTaskType;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Trigger()
    {
        foreach (DailyTask dailyTask in DayManager.Instance.dailyTasks)
        {
            if (dailyTask.dailyTaskType == dailyTaskType)
            {
                if (!dailyTask.complete)
                {
                    if(dailyTask.hintAudio)
                    {
                        if (DayManager.Instance.muffledSound)
                            SoundManager.Instance.GetComponent<AudioSource>().clip = dailyTask.hintAudioMuffled;
                        else
                            SoundManager.Instance.GetComponent<AudioSource>().clip = dailyTask.hintAudio;
                        SoundManager.Instance.GetComponent<AudioSource>().Play();
                    }
                    dailyTask.DisplayHint();
                }
            }
        }
    }

    public void Interact()
    {
        foreach(DailyTask dailyTask in DayManager.Instance.dailyTasks)
        {
            if(dailyTask.dailyTaskType == dailyTaskType)
            {
                    dailyTask.StartTask();
            }
        }
        
    }
}
