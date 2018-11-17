using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DailyTaskType
{
    TurnOnRadioTask,
    WarteringPlantTask,
    Sleep
}

public class DailyTask : MonoBehaviour {

    public bool complete;
    public DailyTaskType dailyTaskType;
    public float animationTime = 2.5f;

    public AudioClip hintAudio;
    public AudioClip progressAudio;

    public string hintText;
    public string succesText;

    //Animator anim;

	// Use this for initialization
	void Start () {	
        //anim=character.GetComponent<Animator>();	
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
                StartCoroutine(WateringCoroutine(animationTime));
                break;
            case DailyTaskType.Sleep:
                StartCoroutine(DayManager.Instance.NextDay());
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
    }

    public IEnumerator WateringCoroutine(float time)
    {
        //anim.SetBool("isWateringPlant",true);
        SoundManager.Instance.GetComponent<AudioSource>().clip = progressAudio;
        SoundManager.Instance.GetComponent<AudioSource>().Play();
        yield return new WaitForSecondsRealtime(time);
        StartCoroutine(FloatingTextManager.Instance.DisplayHideText(succesText));
        DayManager.Instance.IncrementTask();
        complete = true;
        //anim.SetBool("isWateringPlant",false);
    }
}
