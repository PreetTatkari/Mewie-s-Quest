using UnityEngine;
using UnityEngine.Video;  // Import the VideoPlayer namespace
using UnityEngine.SceneManagement;  // Import the SceneManager namespace
using UnityEngine.UI;  // Import the UI namespace
using System.Collections.Generic;  // For using List

public class ShowerEnemy : MonoBehaviour
{
    public int maxHP = 50;                // Maximum health points of the shower enemy
    private int currentHP;                // Current health points of the shower enemy

    // Define tags for two types of projectiles
    public string primaryProjectileTag = "PrimaryProjectile";  // Tag of the primary projectile
    public string secondaryProjectileTag = "SecondaryProjectile";  // Tag of the secondary projectile

    // Define damage values for the two types of projectiles
    public int primaryProjectileDamage = 25;  // Damage dealt by the primary projectile
    public int secondaryProjectileDamage = 10;  // Damage dealt by the secondary projectile

    public VideoPlayer videoPlayer;       // Reference to the VideoPlayer component
    public string nextSceneName;          // Name of the scene to switch to after the video ends
    public List<GameObject> uiElements;  // List to store UI elements
    public Text hpText;                   // Reference to the Text component for displaying HP
    public Slider healthBar;              // Reference to the Slider component for the health bar

    private void Start()
    {
        currentHP = maxHP;  // Set current HP to maximum HP at the start
        UpdateHPUI();       // Update the HP UI initially
        videoPlayer.loopPointReached += ResumeGameAfterVideo;  // Subscribe to video end event
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the triggering object is a primary projectile
        if (other.CompareTag(primaryProjectileTag))
        {
            TakeDamage(primaryProjectileDamage);    // Damage the shower by the primary projectile's damage
            Destroy(other.gameObject);  // Destroy the primary projectile after hitting the shower
        }
        // Check if the triggering object is a secondary projectile
        else if (other.CompareTag(secondaryProjectileTag))
        {
            TakeDamage(secondaryProjectileDamage);  // Damage the shower by the secondary projectile's damage
            Destroy(other.gameObject);  // Destroy the secondary projectile after hitting the shower
        }
    }

    // Function to reduce HP and check for destruction
    private void TakeDamage(int damageAmount)
    {
        currentHP -= damageAmount;  // Reduce current HP by the damage amount
        currentHP = Mathf.Clamp(currentHP, 0, maxHP); // Ensure HP doesn't go below 0
        UpdateHPUI();               // Update the HP UI after taking damage

        // Log the current HP for debugging
        Debug.Log("Shower HP: " + currentHP);

        // If HP is less than or equal to 0, destroy the shower
        if (currentHP <= 0)
        {
            Debug.Log("Shower Destroyed");
            DestroyShower();
        }
    }

    // Function to destroy the shower
    private void DestroyShower()
    {
        // Optionally add effects like explosion, sound, etc. here

        // Disable HP UI elements
        if (hpText != null)
            hpText.gameObject.SetActive(false);
        if (healthBar != null)
            healthBar.gameObject.SetActive(false);

        // Pause the game and play the video cutscene
        PauseGameAndPlayVideo();
        
        // Destroy the shower game object
        Destroy(gameObject);
    }

    // Function to pause the game and play the video cutscene
    private void PauseGameAndPlayVideo()
    {
        Time.timeScale = 0f;  // Pause the game
        videoPlayer.gameObject.SetActive(true);  // Activate and start playing the video
        videoPlayer.Play();  // Start the video

        // Disable UI elements
        SetUIActive(false);
    }

    // Function to resume the game and switch to a new scene after the video ends
    private void ResumeGameAfterVideo(VideoPlayer vp)
    {
        Time.timeScale = 1f;  // Resume the game
        videoPlayer.gameObject.SetActive(false);  // Hide the video player

        // Enable UI elements
        SetUIActive(true);

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }

    // Function to set the UI elements active or inactive
    private void SetUIActive(bool active)
    {
        foreach (GameObject uiElement in uiElements)
        {
            uiElement.SetActive(active);
        }
    }

    // Function to update the HP UI
    private void UpdateHPUI()
    {
        if (hpText != null)
        {
            hpText.text = "Shower HP: " + currentHP;  // Set the text to display the current HP
        }
        if (healthBar != null)
        {
            healthBar.value = (float)currentHP / maxHP; // Normalize HP to a 0-1 range for the slider
        }
    }
}
