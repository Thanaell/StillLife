using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyTask : MonoBehaviour {

    public bool complete;
    public DailyTaskType dailyTaskType;

	// Use this for initialization
	void Start () {		
	}
	
	// Update is called once per frame
	void Update () {		
	}

    public void StartTask()
    {
        switch (dailyTaskType)
        {
            case DailyTaskType.TurnOnRadioTask:
                Debug.Log("C'EST MOI QUI ALLUME");

                break;

            case DailyTaskType.WarteringPlantTask:
                Debug.Log("C'EST MOI QUI ARROSE");
                break;
            default:
                break;
        }
        DayManager.Instance.IncrementTask();
        complete = true;
    }
}
