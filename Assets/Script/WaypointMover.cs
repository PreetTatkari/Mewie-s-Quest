using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    public Transform[] waypoints; // Array to hold the waypoints
    public float speed = 5f; // Speed at which the object moves
    private int currentWaypointIndex = 0; // Index of the current waypoint
    private bool isReversing = false; // Flag to indicate whether the object is reversing

    void Update()
    {
        // Move towards the current waypoint
        MoveTowardsWaypoint();

        // Check if the object has reached the current waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            // If we are not reversing, move to the next waypoint, otherwise move backward
            if (!isReversing)
            {
                currentWaypointIndex++;
                // If we've reached the last waypoint, start reversing
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = waypoints.Length - 1;
                    isReversing = true;
                }
            }
            else
            {
                currentWaypointIndex--;
                // If we've reached the first waypoint, stop reversing
                if (currentWaypointIndex < 0)
                {
                    currentWaypointIndex = 0;
                    isReversing = false;
                }
            }
        }
    }

    void MoveTowardsWaypoint()
    {
        // Calculate the direction to the current waypoint
        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;
        
        // Move the object towards the current waypoint
        transform.position += direction * speed * Time.deltaTime;
    }
}
