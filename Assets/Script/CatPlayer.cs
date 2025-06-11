using UnityEngine;
using UnityEngine.UI;   // Import UI namespace
using UnityEngine.SceneManagement;   // Import Scene Management namespace

public class CatPlayer : MonoBehaviour
{
    public int maxHP = 10;         // Maximum health points of the cat
    private int currentHP;         // Current health points of the cat

    public Text hpText;            // Reference to the Text component for displaying HP
    public Slider healthBar;       // Reference to the Slider component for the health bar
    public string gameOverSceneName;  // Name of the scene to load when the game is over

    // Tags for objects that can damage the cat
    public string damagingTag = "DamagingObject";
    public string specialDamagingTag = "SpecialDamagingObject";

    private void Start()
    {
        currentHP = maxHP;         // Set current HP to maximum HP at the start
        UpdateHPUI();              // Update the UI with initial HP
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the triggering object has the special tag for higher damage
        if (other.CompareTag(specialDamagingTag))
        {
            TakeDamage(25);    // Damage the cat by 25 HP from special objects
        }
        // Check if the triggering object has the regular damaging tag
        else if (other.CompareTag(damagingTag))
        {
            TakeDamage(10);    // Damage the cat by 10 HP from regular objects
        }
    }

    // Function to reduce HP and check for game over
    private void TakeDamage(int damageAmount)
    {
        currentHP -= damageAmount;     // Reduce the current HP by the damage amount
        currentHP = Mathf.Clamp(currentHP, 0, maxHP); // Ensure HP doesn't go below 0
        Debug.Log("Cat HP: " + currentHP);
        UpdateHPUI();                  // Update the HP in the UI

        // If HP drops to 0 or below, stop the game
        if (currentHP <= 0)
        {
            Debug.Log("Game Over");
            StopGame();
        }
    }

    // Function to update the HP UI
    private void UpdateHPUI()
    {
        if (hpText != null)
        {
            hpText.text = "Cat HP: " + currentHP;  // Set the text to display the current HP
        }
        if (healthBar != null)
        {
            healthBar.value = (float)currentHP / maxHP; // Normalize HP to a 0-1 range for the slider
        }
    }

    // Function to stop the game and switch scenes
    private void StopGame()
    {
        // Optionally, freeze the time (can be removed if not needed)
        Time.timeScale = 0;

        // Switch to the specified scene
        SceneManager.LoadScene(gameOverSceneName);
    }
}
