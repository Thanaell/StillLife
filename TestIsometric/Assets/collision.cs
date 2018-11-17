using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour {

	Vector3 speed;
	public float maxSpeed=5.0f;
	bool canInteract;

	void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.tag == "Interactable")
        {
            canInteract=true;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal_movement=Input.GetAxis("Horizontal");
		float vertical_movement=Input.GetAxis("Vertical");

		speed.x=horizontal_movement;
		speed.z=vertical_movement;

		if(canInteract && Input.GetKeyDown(KeyCode.Space)){
			Debug.Log("I INTERACTED");
		}

		transform.Translate(speed * maxSpeed * Time.deltaTime, Space.Self);
	}
	
}
