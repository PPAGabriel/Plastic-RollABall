using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    private NavMeshAgent pathfinder; // Reference to the navmesh agent
    private Transform target; // Reference to the player
	private PlayerController playerController; // Reference to the player controller
    
    void Start()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
		playerController = target.GetComponent<PlayerController>();
    }
    void Update()
    {
		if (playerController.count >= 12) // Check if the player has collected all the pickups
	    {
		    pathfinder.isStopped = true; // Stop the enemy
		}
		else{
        	pathfinder.SetDestination(target.position);
		}
    }
}
