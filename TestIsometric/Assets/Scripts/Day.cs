using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Day : MonoBehaviour {
    public int dayNumber;
    public PostProcessingProfile postProcessingProfile;
    public float inputDelay;
    public bool interactionSpam;
    public bool inversedInput;
    public bool muffledSound;
    public bool speedyDay;
    public AudioClip audioclip;
    public string plantHint;
    public string plantFail;
    public string radioHint;
    public string radioFail;
    public string pillsHint;
    public string pillsFail;
}
