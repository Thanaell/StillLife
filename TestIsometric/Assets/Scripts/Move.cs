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

            rb.MovePosition(transform.position + rightMovement + upMovement);

            if(upMovement!=Vector3.zero){
			    if(rightMovement.x>0){
				    //spriteR.sprite=sprites[1];
                    anim.SetBool("isMovingRight",true);
                    //anim.SetTrigger("moveHorizontal");
			    }
			    else if (rightMovement.x<0){
                    anim.SetBool("isMovingLeft",true);
                }
                else if (upMovement.z>0){
				    //spriteR.sprite=sprites[0];
                    Debug.Log("Up" + upMovement);
				    anim.SetBool("isMovingUp",true);
                    anim.SetTrigger("moveFront");
			    }
                else if (upMovement.z<0){
                    anim.SetBool("isMovingRight",true);
                }
		    }
		    else if(rightMovement.x>0){
                Debug.Log("side" + rightMovement);
                anim.SetBool("isMovingRight", true);
                anim.SetTrigger("moveSide");
		    }
            else if(rightMovement.x<0){
                anim.SetBool("isMovingLeft", true);
            }
        }


        if (canInteract && Input.GetKeyUp(KeyCode.Space) && interactiveObject)
        {
            Debug.Log("I INTERACTED");
            interactiveObject.Interact();
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
