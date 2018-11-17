using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    float moveSpeed = 5f;
    bool canInteract = false;

    Vector3 forward, right, lastPosition;

    string spriteNames="diamonds3sprites";
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
        anim.SetBool("isMovingDiagonal",false);
        anim.SetBool("isMovingFront",false);
        anim.SetBool("isMovingSide",false);

    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate()
    {
        lastPosition = transform.position;
        anim.SetBool("isMovingDiagonal",false);
        anim.SetBool("isMovingFront",false);
        anim.SetBool("isMovingSide",false);
        if (Input.anyKey)
        {
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

            Rigidbody rb = GetComponent<Rigidbody>();

            rb.MovePosition(transform.position + rightMovement + upMovement);

            if(upMovement!=Vector3.zero){
			    if(rightMovement!=Vector3.zero){
				    //spriteR.sprite=sprites[1];
                    anim.SetBool("isMovingDiagonal",true);
                    anim.SetTrigger("moveDiagonal");
			    }
			    else{
				    //spriteR.sprite=sprites[0];
                    Debug.Log("Up" + upMovement);
				    anim.SetBool("isMovingFront",true);
                    anim.SetTrigger("moveFront");
			    }
		    }
		    else if(rightMovement!=Vector3.zero){
                Debug.Log("side" + rightMovement);
                anim.SetBool("isMovingSide", true);
                anim.SetTrigger("moveSide");
		    }

            //if(upMovement==Vector3.zero){
                //Debug.Log("Up"+ upMovement);
                //anim.SetBool("isMovingFront",false);
                //anim.SetBool("isMovingDiagonal",false);
            //}
            //if(rightMovement==Vector3.zero){
            //    Debug.Log("side" + rightMovement);
                //anim.SetBool("isMovingSide",false);
                //anim.SetBool("isMovingDiagonal",false);
            //}

        if (canInteract && Input.GetKeyUp(KeyCode.Space) && interactiveObject)
        {
            Debug.Log("I INTERACTED");
            interactiveObject.Interact();
        }
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
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Interactable")
        {
            canInteract = false;
        }
    }
}
