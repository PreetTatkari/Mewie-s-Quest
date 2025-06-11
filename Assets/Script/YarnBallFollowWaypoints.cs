using UnityEngine;

public class YarnBallFollowWaypoints : MonoBehaviour
{
    public Transform[] waypoints;  // Array of waypoints for the yarn ball to follow
    public float followForce = 10f;  // The force applied to move the ball
    public float maxSpeed = 5f;  // Maximum speed of the yarn ball
    public float waypointRadius = 1f;  // Radius within which the ball is considered to have reached a waypoint

    private Rigidbody rb;
    private int currentWaypointIndex = 0;  // Index of the current waypoint the ball is moving towards

    void Start()
    {
        // Get the Rigidbody component attached to the yarn ball
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Follow the waypoints continuously
        FollowWaypoints();
    }

    void FollowWaypoints()
    {
        // Check if there are any waypoints to follow
        if (waypoints.Length == 0) return;

        // Get the direction towards the current waypoint
        Vector3 directionToWaypoint = (waypoints[currentWaypointIndex].position - transform.position).normalized;

        // Apply force to move the ball towards the current waypoint
        rb.AddForce(directionToWaypoint * followForce);

        // Cap the speed of the yarn ball
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // Check if the ball is within the radius of the current waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < waypointRadius)
        {
            // Move to the next waypoint
            currentWaypointIndex++;

            // If we reached the last waypoint, loop back to the first one
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }
}