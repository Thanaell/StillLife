using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DailyTaskType
{
    TurnOnRadioTask,
    WarteringPlantTask,
    Pills,
    Sleep,
    Journal
}

public class DailyTask : MonoBehaviour {

    public bool complete;
    public DailyTaskType dailyTaskType;
    public float animationTime = 2.5f;

    public AudioClip hintAudio;
    public AudioClip progressAudio;
    public AudioClip hintAudioMuffled;
    public AudioClip progressAudioMuffled;

  

    public string hintText;
    public string succesText;

    Animator anim;
    GameObject character;

	// Use this for initialization
	void Start () {	
        character = GameObject.Find("Character");
        anim=character.GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void Update () {		
	}

    public void StartTask()
    {
        FloatingTextManager.Instance.HideText();

        switch (dailyTaskType)
        {
            case DailyTaskType.TurnOnRadioTask:
                Debug.Log("C'EST MOI QUI ALLUME");
                StartCoroutine(TurnOnRadio());
                break;

            case DailyTaskType.WarteringPlantTask:
                Debug.Log("C'EST MOI QUI ARROSE");
                StartCoroutine(WateringCoroutine(animationTime));
                break;
            case DailyTaskType.Sleep:
                StartCoroutine(DayManager.Instance.NextDay());
                break;
            case DailyTaskType.Pills:
                Debug.Log("JE PRENDS MES MEDOCS");
                StartCoroutine(ChoosePill());
                break;
            case DailyTaskType.Journal:
                StartCoroutine(ReadJournal());
                break;
            default:
                DayManager.Instance.IncrementTask();
                complete = true;
                break;
        }
    }

    public void DisplayHint()
    {
        FloatingTextManager.Instance.DisplayText(hintText);

        switch (dailyTaskType)
        {
            case DailyTaskType.TurnOnRadioTask:
                break;
            case DailyTaskType.WarteringPlantTask:
                StartCoroutine(FloatingTextManager.Instance.DisplayHideText(DayManager.Instance.currentDay.plantHint));
                break;
            case DailyTaskType.Sleep:
                break;
            case DailyTaskType.Pills:
                StartCoroutine(FloatingTextManager.Instance.DisplayHideText(DayManager.Instance.currentDay.pillsHint));
                break;
            case DailyTaskType.Journal:
                StartCoroutine(FloatingTextManager.Instance.DisplayHideText(DayManager.Instance.currentDay.journalHint));
                break;
            default:
                break;
        }
    }

    public IEnumerator WateringCoroutine(float time)
    {
        anim.SetBool("isWateringPlant",true);
        Debug.Log(anim.GetBool("isWateringPlant"));
        if(DayManager.Instance.muffledSound)
            SoundManager.Instance.GetComponent<AudioSource>().clip = progressAudioMuffled;  
        else
            SoundManager.Instance.GetComponent<AudioSource>().clip = progressAudio;
        SoundManager.Instance.GetComponent<AudioSource>().Play();
        yield return new WaitForSecondsRealtime(time);
        StartCoroutine(FloatingTextManager.Instance.DisplayHideText(succesText));
        DayManager.Instance.IncrementTask();
        complete = true;
        anim.SetBool("isWateringPlant",false);
    }

    public IEnumerator TurnOnRadio()
    {
        string[] sentences = DayManager.Instance.currentDay.radioHint.Split('.');
        anim.SetBool("isActivatingRadio",true);
        anim.SetTrigger("activateRadio");
        SoundManager.Instance.GetComponent<AudioSource>().clip = DayManager.Instance.currentDay.radioClip;
        SoundManager.Instance.GetComponent<AudioSource>().Play();
        complete = true;
        foreach (string s in sentences)
        {
            StartCoroutine(FloatingTextManager.Instance.DisplayHideText(s, 5f));
            yield return new WaitForSecondsRealtime(5.1f);
        }
        DayManager.Instance.IncrementTask();
    }

    public IEnumerator ReadJournal()
    {
        string[] sentences = DayManager.Instance.currentDay.journalHint.Split('.');
        SoundManager.Instance.GetComponent<AudioSource>().clip = DayManager.Instance.currentDay.journalClip;
        SoundManager.Instance.GetComponent<AudioSource>().Play();
        DayManager.Instance.journalCanvas.SetActive(true);
        complete = true;
        foreach (string s in sentences)
        {
            StartCoroutine(FloatingTextManager.Instance.DisplayHideText(s, 5f));
            yield return new WaitForSecondsRealtime(5.1f);
        }
        DayManager.Instance.IncrementTask();
    }

    public IEnumerator ChoosePill()
    {
        bool pillsTaken = false;
        SoundManager.Instance.GetComponent<AudioSource>().clip = DayManager.Instance.currentDay.pillsClip;
        SoundManager.Instance.GetComponent<AudioSource>().Play();
        DayManager.Instance.pillCanvas.SetActive(true);
        string succesText = "";
        while(!pillsTaken)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                complete = true;
                DayManager.Instance.pillCanvas.SetActive(false);
                pillsTaken = true;
                succesText = "Bravo, tu sais toujours lire les couleurs...";
            }
            else if (Input.anyKeyDown)
            {
                Debug.Log("t'es nul ou quoi ?");
                pillsTaken = true;
                DayManager.Instance.pillCanvas.SetActive(false);
                succesText = "Raté ! Je suis immortel de toute façon";
            }
            yield return null;
        }
        yield return new WaitForSecondsRealtime(0.2f);
        StartCoroutine(FloatingTextManager.Instance.DisplayHideText(succesText));
        DayManager.Instance.IncrementTask();
    }
}
