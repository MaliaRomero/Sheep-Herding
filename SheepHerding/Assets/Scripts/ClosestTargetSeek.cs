using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClosestTargetSeek : MonoBehaviour
{
    public List<Transform> targets;  // List of targets (sheep)
    public Transform player;         // Reference to the player (the one to flee from)
    public float maxSpeed = 5f;
    public KinematicFlee fleeScript; // Reference to KinematicFlee script
    public float fleeDistance = 5f;  // Distance at which the wolf starts fleeing

    private bool isFleeing = false;  // Track whether we are fleeing
    public TextMeshProUGUI loseText;
    void Start()
    {
        fleeScript.enabled = false; // Initially disable flee behavior
    }

    void Update()
    {   
        RemoveInactiveTargets();

        if (targets.Count == 0)
        {
            endGame();
            return;  // No targets to seek, exit the update method early
        }
        
        // Find the closest target (either sheep or player)
        Transform closestTarget = GetClosestTarget();

        // Check if the player is within fleeing distance
        if (Vector3.Distance(transform.position, player.position) <= fleeDistance)
        {
            if (targets.Count > 1) {
                if (!isFleeing)
                {
                    fleeScript.enabled = true;  // Enable flee behavior
                    isFleeing = true;  // Mark as fleeing
                }
            }

        }
        else if (isFleeing)
        {
            fleeScript.enabled = false;  // Disable flee behavior
            isFleeing = false;  // Mark as not fleeing
        }

        // If we're not fleeing, perform seeking behavior
        if (!isFleeing)
        {
            Vector3 direction = closestTarget.position - transform.position;
            direction.Normalize();  // Making it a unit vector

            Vector3 velocity = direction * maxSpeed;
            transform.position += velocity * Time.deltaTime;

            // Face in the direction we want to move
            transform.LookAt(closestTarget);
        }
    }

    // Function to find the closest target from the list of sheep
    Transform GetClosestTarget()
    {
        Transform closestTarget = targets[0];
        float closestDistance = Vector3.Distance(transform.position, closestTarget.position);

        foreach (Transform target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
            }
        }

        return closestTarget;
    }

    void RemoveInactiveTargets()
    {
        targets.RemoveAll(target => !target.gameObject.activeSelf);
    } 

    void endGame(){
        Debug.Log("No Sheep");
        loseText.gameObject.SetActive(true);
    }
}