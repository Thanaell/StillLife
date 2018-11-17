using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Day : MonoBehaviour {
    public int dayNumber;

    public PostProcessingProfile postProcessingProfile;
    public float inputDelay;
    public float blurringLevel;
    public bool inversedInput;
    public float noiseLevel;
    public float fieldOfView;
    public AudioClip audioclip;
}
