using UnityEngine;

public class ShowerRotation : MonoBehaviour
{
    public Transform centerPoint; // Assign this in the inspector
    public float rotationSpeed = 20f; // Speed of the rotation
    public float changeInterval = 2f; // Time in seconds to change direction
    private float timeUntilChange;
    private int direction = 1; // 1 for clockwise, -1 for counterclockwise

    void Start()
    {
        // Set initial time until direction change
        timeUntilChange = changeInterval;
    }

    void Update()
    {
        // Rotate around the center point with random direction (clockwise or counterclockwise)
        transform.RotateAround(centerPoint.position, Vector3.up, direction * rotationSpeed * Time.deltaTime);

        // Ensure the shower is always facing the center point
        transform.LookAt(centerPoint);

        // Update the time until the next direction change
        timeUntilChange -= Time.deltaTime;
        if (timeUntilChange <= 0)
        {
            // Randomly set direction to clockwise or counterclockwise
            direction = Random.Range(0, 2) == 0 ? 1 : -1;

            // Reset the timer
            timeUntilChange = changeInterval;
        }
    }
}
