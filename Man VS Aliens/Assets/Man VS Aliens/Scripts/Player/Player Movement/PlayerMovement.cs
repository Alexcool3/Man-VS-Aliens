using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Movement attributes
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 60.0f;
    public bool canMoveSideways = false;
    // Jumoing attributes
    public float jumpSpeed = 0;

    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        // Basic Movement
        
        if(Input.GetKey(KeyCode.W))
        {
            // Deub.Log("Keyboard 'w' pressed")
            this.transform.Translate(new Vector3(0, 0, moveSpeed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // Deub.Log("Keyboard 's' pressed")
            this.transform.Translate(new Vector3(0, 0, -moveSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.A))
        {
            // Deub.Log("Keyboard 'a' pressed")
            this.transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0 ,0));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // Deub.Log("Keyboard 'd' pressed")
            this.transform.Translate(new Vector3(moveSpeed * Time.deltaTime,0,0));
        }

        // Jump Movement

        /* In order to jump make sure to check 
         * whether the player is on on the floor or not.
        */
      
        if (Input.GetKey(KeyCode.Space))
        {
           GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed);

        }
            
        
       
       


    }
}
