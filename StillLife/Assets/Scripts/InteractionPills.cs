using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPills : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("bonne pilule");
        }
        else if (Input.anyKeyDown)
        {
            Debug.Log("t'es nul ou quoi ?");
        }
    }
}
