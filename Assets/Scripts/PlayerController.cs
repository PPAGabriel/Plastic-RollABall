using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; // Reference to the rigidbody component
    private float movementX; // Movement in the x-axis
    private float movementY; // Movement in the y-axis
    public float speed = 0; // Speed of the player
    public float jumpForce = 5.0f; // Force of the jump
	public int count; // Number of pickups collected
	public TextMeshProUGUI countText; // Reference to the count text
	public GameObject winTextObject; // Reference to the win text
	private Animator anim; // Reference to the animator component
    
    // Start is called before the first frame update
    void Start()
    {
		winTextObject.SetActive(false); // Deactivate the win text
		SetCountText(); // Set the count text at the start
		count = 0; // Initialize the count
        rb = GetComponent<Rigidbody>(); // Get the rigidbody component
		anim = GetComponent<Animator>(); // Get the animator component
    }
    
    // Function that moves the player
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>(); // Get the movement vector
        
        movementX = movementVector.x; // Set the movement in the x-axis
        movementY = movementVector.y; // Set the movement in the y-axis
    }
	
	// Function that updates the count text
	void SetCountText()
	{
	    countText.text = "Count: " + count.ToString(); // Update the count text
		
		if (count >= 12) // Check if the player has collected all the pickups
		{
            winTextObject.SetActive(true); // Activate the win text
        }
	}

    private void FixedUpdate()
    {
        Vector3 movement=new Vector3(movementX,0.0f,movementY); // Create a vector with the movement
        rb.AddForce(movement*speed); // Apply the force to the rigidbody
        
        // Jump if the space key is pressed
	    if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }
    
    // Function that jumps the player
    void Jump()
    {
	    // Check if the player is on the ground before jumping
	    if (Mathf.Abs(rb.velocity.y) < 0.01f)
	    {
		    // Apply an impulse force to the player to make it jump
		    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
	    }
    }

	// Function that checks for collisions
	void OnTriggerEnter(Collider other) 
   {
   		if (other.gameObject.CompareTag("PickUp")) // Check if the object collided is a pickup
        {
           other.gameObject.SetActive(false); // Deactivate the object
		   count = count + 1; // Increase the count
		   SetCountText(); // Update the count text

			anim.SetBool("isEating", true); // Set the isJumping parameter to true
	        StartCoroutine(StopEatingAfterSeconds(0.2f)); // Wait for 2 seconds
        
        }
	}

	// Coroutine that waits for a few seconds
	IEnumerator StopEatingAfterSeconds(float seconds)
	{
    	yield return new WaitForSeconds(seconds);
    	anim.SetBool("isEating", false);
	}
}
