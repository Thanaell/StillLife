using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class DayManager : Singleton<DayManager> {
    public Day[] days;
    public DailyTask[] dailyTasks;
    public int numberOfTasksCompleted;

    public Day currentDay;
    public float inputDelay;
    public float blurringLevel;
    public bool inversedInput;
    public bool muffledSound;
    public float fieldOfView;
    public float nightTime = 4f;

    public Canvas nightCanvas;

    protected DayManager() { }

    private int dayIndex;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        dayIndex = 0;
        numberOfTasksCompleted = 0;
        currentDay = days[0];
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = currentDay.audioclip;
        audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
    }

    public IEnumerator NextDay()
    {
        StartCoroutine(ScreenFadeIn(nightTime / 2));
        yield return new WaitForSecondsRealtime(nightTime);
        dayIndex++;
        numberOfTasksCompleted = 0;
        currentDay = days[dayIndex];
        audioSource.clip = currentDay.audioclip;
        audioSource.Play();
        inputDelay = currentDay.inputDelay;
        blurringLevel = currentDay.blurringLevel;
        inversedInput = currentDay.inversedInput;
        muffledSound = currentDay.muffledSound;
        fieldOfView = currentDay.fieldOfView;
        Camera.main.GetComponent<PostProcessingBehaviour>().profile = currentDay.postProcessingProfile;

        foreach(DailyTask dailyTask in dailyTasks)
        {
            dailyTask.complete = false;
        }
    }

    public void IncrementTask()
    {
        numberOfTasksCompleted++;

        //if (numberOfTasksCompleted == dailyTasks.Length)
        //{
        //    DayManager.Instance.NextDay();
        //}
    }

    public IEnumerator ScreenFadeIn(float fadeInTime)
    {
        nightCanvas.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeInTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(0, 1f, t));
            nightCanvas.GetComponentInChildren<Image>().color = newColor;
            yield return new WaitForEndOfFrame();
        }
        nightCanvas.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 1f);
        StartCoroutine(ScreenFadeOut(nightTime / 2));
    }

    public IEnumerator ScreenFadeOut(float fadeOutTime)
    {
        nightCanvas.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 1f);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeOutTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(1, 0, t));
            nightCanvas.GetComponentInChildren<Image>().color = newColor;
            yield return new WaitForEndOfFrame();
        }
        nightCanvas.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0);
    }
}
