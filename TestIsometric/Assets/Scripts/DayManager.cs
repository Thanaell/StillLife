using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class DayManager : Singleton<DayManager> {
    public Day[] days;
    public DailyTask[] dailyTasks;
    public int numberOfTasksCompleted;

    public Day currentDay;
    public float inputDelay;
    public float blurringLevel;
    public bool inversedInput;
    public float noiseLevel;
    public float fieldOfView;

    protected DayManager() { }

    private int dayIndex;

    // Use this for initialization
    void Start () {
        dayIndex = 0;
        numberOfTasksCompleted = 0;
        currentDay = days[0];
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void NextDay()
    {
        dayIndex++;
        numberOfTasksCompleted = 0;
        currentDay = days[dayIndex];
        inputDelay = currentDay.inputDelay;
        blurringLevel = currentDay.blurringLevel;
        inversedInput = currentDay.inversedInput;
        noiseLevel = currentDay.noiseLevel;
        fieldOfView = currentDay.fieldOfView;
        Camera.main.GetComponent<PostProcessingBehaviour>().profile = currentDay.postProcessingProfile;
    }

    public void IncrementTask()
    {
        numberOfTasksCompleted++;

        if (numberOfTasksCompleted == dailyTasks.Length)
        {
            DayManager.Instance.NextDay();
        }
    }
}
