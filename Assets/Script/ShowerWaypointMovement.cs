using UnityEngine;

public class ShowerWaypointMovement : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints assigned in the inspector
    public float moveSpeed = 5f;  // Speed of the shower moving between waypoints
    public float rotationSpeed = 2f; // Speed of rotation towards next waypoint
    private int currentWaypointIndex = 0;

    void Update()
    {
        MoveAlongWaypoints();
    }

    void MoveAlongWaypoints()
    {
        if (waypoints.Length == 0)
            return;

        // Get the current target waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Move towards the target waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

        // Rotate to face the target waypoint
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // If the shower reaches the current waypoint, move to the next one
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // Loop through waypoints
        }
    }
}
