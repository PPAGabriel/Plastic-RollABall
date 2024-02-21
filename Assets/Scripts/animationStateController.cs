using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    public Animator anim; // Reference to the animator component
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); // Get the animator component
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("s"); // Check if the s key is pressed
        bool runPressed = Input.GetKey("left shift"); // Check if the left shift key is pressed

        bool isWalking=anim.GetBool("isWalking");
        bool isRunning=anim.GetBool("isRunning");
        
        if (!isWalking && forwardPressed) // Check if the player is not walking and the s key is pressed
        {
            anim.SetBool("isWalking", true); // Set the isWalking parameter to true
        }
        else if (isWalking && !forwardPressed) // Check if the player is walking and the s key is not pressed
        {
            anim.SetBool("isWalking", false); // Set the isWalking parameter to false
        }
        
        if (forwardPressed && runPressed) // Check if the s key and the left shift key are pressed
        {
            anim.SetBool("isRunning", true); // Set the isRunning parameter to true
        }
        else
        {
            anim.SetBool("isRunning", false); // Set the isRunning parameter to false
        }
        
        
        if (Input.GetKey(KeyCode.Space)) // Check if the space key is pressed
        {
            anim.SetBool("isJumping", true); // Set the isJumping parameter to true
        }
        else
        {
            anim.SetBool("isJumping", false); // Set the isJumping parameter to false
        }
    }
}
