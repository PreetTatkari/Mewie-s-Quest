using System.Collections;
using UnityEngine;

public class BubbleDisappear : MonoBehaviour
{
    public float disappearDuration = 3f; // Time the bubbles will be invisible
    public float visibleDuration = 5f;   // Time the bubbles will be visible
    public float randomStartOffset = 2f; // Random start delay for bubbles

    private Renderer bubbleRenderer;     // To control visibility
    private Collider bubbleCollider;     // Optional: To disable interactions
    private bool isVisible = true;

    void Start()
    {
        // Get the Renderer and Collider components from the bubble object
        bubbleRenderer = GetComponent<Renderer>();
        bubbleCollider = GetComponent<Collider>();

        // Start the cycle of visibility and disappearance with a random offset
        StartCoroutine(HandleVisibilityCycle());
    }

    IEnumerator HandleVisibilityCycle()
    {
        // Wait for a random time before starting the visibility cycle
        yield return new WaitForSeconds(Random.Range(0f, randomStartOffset));

        while (true)
        {
            // Toggle visibility
            isVisible = !isVisible;

            if (isVisible)
            {
                bubbleRenderer.enabled = true;
                if (bubbleCollider != null)
                    bubbleCollider.enabled = true;

                yield return new WaitForSeconds(visibleDuration);
            }
            else
            {
                bubbleRenderer.enabled = false;
                if (bubbleCollider != null)
                    bubbleCollider.enabled = false;

                yield return new WaitForSeconds(disappearDuration);
            }
        }
    }
}
