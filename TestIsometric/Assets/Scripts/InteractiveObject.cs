using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DailyTaskType
{
    TurnOnRadioTask,
    WarteringPlantTask
}

public class InteractiveObject : MonoBehaviour {
    public DailyTaskType dailyTaskType;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Interact()
    {
        foreach(DailyTask dailyTask in DayManager.Instance.dailyTasks)
        {
            if(dailyTask.dailyTaskType == dailyTaskType)
            {
                if(!dailyTask.complete)
                    dailyTask.StartTask();
            }
        }
        
    }
}
