using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour {

	Vector3 speed;
	public float maxSpeed=5.0f;
	bool canInteract;
	string spriteNames="diamonds3sprites";
	SpriteRenderer spriteR;
	Sprite[] sprites;

	void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.tag == "Interactable")
        {
            canInteract=true;
        }
    }

	// Use this for initialization
	void Start () {
		spriteR=gameObject.GetComponent<SpriteRenderer>();
		sprites=Resources.LoadAll<Sprite>(spriteNames);
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


		if(speed.z!=0){
			if(speed.x!=0){
				spriteR.sprite=sprites[1];
			}
			else{
				spriteR.sprite=sprites[0];
			}
		}
		else if(speed.x!=0){
			spriteR.sprite=sprites[2];
		}

		transform.Translate(speed * maxSpeed * Time.deltaTime, Space.Self);
	}
	
}
