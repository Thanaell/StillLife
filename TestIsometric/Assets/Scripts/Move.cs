using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    float moveSpeed = 2f;
    bool canInteract = false;

    Vector3 forward, right, lastPosition;

    string spriteNames="SpriteSheet2";
	SpriteRenderer spriteR;
	Sprite[] sprites;
	Animator anim;

    private InteractiveObject interactiveObject = null;

    bool inversedInput;
    bool interactionSpam;

    int spaceHits;
    bool hasRightToInteract;

    // Use this for initialization
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        spriteR=gameObject.GetComponent<SpriteRenderer>();
		sprites=Resources.LoadAll<Sprite>(spriteNames);
        anim=GetComponent<Animator>();
        anim.SetBool("isMovingRight",false);
        anim.SetBool("isMovingUp",false);
        anim.SetBool("isMovingLeft",false);
        spaceHits=0;

    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate()
    {
        lastPosition = transform.position;
        anim.SetBool("isMovingLeft",false);
        anim.SetBool("isMovingUp",false);
        anim.SetBool("isMovingRight",false);
        if (Input.anyKey)
        {
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");


            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

            Rigidbody rb = GetComponent<Rigidbody>();

            inversedInput=DayManager.Instance.currentDay.inversedInput;
            interactionSpam=DayManager.Instance.currentDay.interactionSpam;

            if(inversedInput){
                rb.MovePosition(transform.position - rightMovement - upMovement);
            }
            else{
                rb.MovePosition(transform.position + rightMovement + upMovement);
            }


            //Goodbye pretty code

            if(upMovement!=Vector3.zero){
			    if(rightMovement.x>0){
				    //spriteR.sprite=sprites[1];
                    if(!inversedInput){ anim.SetBool("isMovingRight",true); }
                    else{ anim.SetBool("isMovingLeft",true);}                    
			    }
			    else if (rightMovement.x<0){
                    if(!inversedInput){ anim.SetBool("isMovingLeft",true); } else { anim.SetBool("isMovingRight",true);}
                }
                else if (upMovement.z>0){
				    if(!inversedInput){ anim.SetBool("isMovingUp",true);} else { anim.SetBool("isMovingRight", true);}
			    }
                else if (upMovement.z<0){
                    if(!inversedInput){ anim.SetBool("isMovingRight",true);} else { anim.SetBool("isMovingUp", true);}
                }
		    }
		    else if(rightMovement.x>0){
                if(!inversedInput){ anim.SetBool("isMovingRight", true);} else { anim.SetBool("isMovingLeft", true);}
		    }
            else if(rightMovement.x<0){
                if(!inversedInput){ anim.SetBool("isMovingLeft", true);} else { anim.SetBool("isMovingRight", true);}
            }
        }

        if(Input.GetKeyUp(KeyCode.Space)){
            spaceHits++;
        }
        canInteract=(!interactionSpam)||(interactionSpam&&spaceHits>5);
        if (canInteract && Input.GetKeyUp(KeyCode.Space) && interactiveObject && canInteract)
        {
            Debug.Log("I INTERACTED");
            interactiveObject.Interact();
            spaceHits=0;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Interactable")
        {
            canInteract = true;
            if (col.gameObject.GetComponent<InteractiveObject>())
            {
                interactiveObject = col.gameObject.GetComponent<InteractiveObject>();
                interactiveObject.Trigger();
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Interactable")
        {
            canInteract = false;
            FloatingTextManager.Instance.HideText();
        }
    }
}
