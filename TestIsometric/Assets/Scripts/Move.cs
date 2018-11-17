using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    float moveSpeed = 5f;
    bool canInteract = false;

    Vector3 forward, right, lastPosition;

    private InteractiveObject interactiveObject = null;

    // Use this for initialization
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        lastPosition = transform.position;
        if (Input.anyKey)
        {
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

            Rigidbody rb = GetComponent<Rigidbody>();

            transform.forward = heading;
            rb.MovePosition(transform.position + rightMovement + upMovement);

            if (canInteract && Input.GetKeyUp(KeyCode.Space) && interactiveObject)
            {
                Debug.Log("I INTERACTED");
                interactiveObject.Interact();    
            }
        }        
    }

    void OnCollisionEnter(Collision col)
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

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Interactable")
        {
            canInteract = false;
        }
    }
}
