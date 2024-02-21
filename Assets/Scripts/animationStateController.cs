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
