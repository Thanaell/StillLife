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
    public bool inversedInput;
    public bool muffledSound;
    public float fieldOfView;
    public float nightTime = 4f;
    public bool interactionSpam;
    public bool speedyDay;

    public Canvas nightCanvas;

    protected DayManager() { }

    private int dayIndex;
    private AudioSource audioSource;


    private void Awake()
    {
        currentDay = days[0];
    }
    // Use this for initialization
    void Start () {
        dayIndex = 0;
        numberOfTasksCompleted = 0;
       
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = currentDay.audioclip;
        audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
    }

    public IEnumerator NextDay()
    {
        foreach (DailyTask dailyTask in dailyTasks)
        {
            if(!dailyTask.complete)
            {
                switch(dailyTask.dailyTaskType)
                {
                    case DailyTaskType.WarteringPlantTask:
                        StartCoroutine(FloatingTextManager.Instance.DisplayHideText(currentDay.plantFail, 2f));
                        yield return new WaitForSecondsRealtime(2.1f);
                        break;
                    case DailyTaskType.TurnOnRadioTask:
                        StartCoroutine(FloatingTextManager.Instance.DisplayHideText(currentDay.radioFail, 2f));
                        yield return new WaitForSecondsRealtime(2.1f);
                        break;
                    case DailyTaskType.Pills:
                        StartCoroutine(FloatingTextManager.Instance.DisplayHideText(currentDay.pillsFail, 2f));
                        yield return new WaitForSecondsRealtime(2.1f);
                        break;
                    default: break;
                }
            }
        }
        StartCoroutine(ScreenFadeIn(nightTime / 2));
        yield return new WaitForSecondsRealtime(nightTime/2);
        dayIndex++;
        numberOfTasksCompleted = 0;
        currentDay = days[dayIndex];
        if(dayIndex==4){ currentDay.inversedInput=true;} //Changement de contrôles au jour 5
        if(dayIndex==3){ currentDay.interactionSpam=true;} //spam la barre d'espace pour interagir au jour 3
        if(dayIndex==2){ currentDay.speedyDay=true;}//le perso a l'air d'aller plus vite aujourd'hui… (joueur 2)
        PlantManager.Instance.ChooseSprite();
        audioSource.clip = currentDay.audioclip;
        audioSource.Play();
        inputDelay = currentDay.inputDelay;
        inversedInput = currentDay.inversedInput;
        muffledSound = currentDay.muffledSound;
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
