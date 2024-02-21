using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; // Reference to the player
    private Vector3 offset; // Offset between the camera and the player
    
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position; // Calculate the offset
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset; // Update the camera position
    }
}
