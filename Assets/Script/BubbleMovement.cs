using System.Collections;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public float moveSpeed = 1f; // Speed of bubble movement
    public float horizontalRange = 1f; // Range of horizontal movement
    public float verticalRange = 1f; // Range of vertical movement
    public float frequency = 1f; // Frequency of oscillation

    private Vector3 startPosition;

    void Start()
    {
        // Store the initial position of the bubble
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate horizontal and vertical movement using sine waves for smooth oscillation
        float horizontalMovement = Mathf.Sin(Time.time * frequency) * horizontalRange;
        float verticalMovement = Mathf.Sin(Time.time * frequency * 2f) * verticalRange;

        // Apply movement to the bubble's position
        transform.position = startPosition + new Vector3(horizontalMovement, verticalMovement, 0) * moveSpeed;
    }
}
