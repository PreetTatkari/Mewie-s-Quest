using UnityEngine;
using UnityEngine.UI;

public class soundManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider; // Reference to the slider
    [SerializeField] private AudioSource audioSource; // Reference to the AudioSource

    private void Start()
    {
        // Initialize the slider value and AudioSource volume
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            // Load the volume setting from PlayerPrefs
            float volume = PlayerPrefs.GetFloat("musicVolume");
            volumeSlider.value = volume;
            audioSource.volume = volume;
        }
        else
        {
            // Set default volume
            volumeSlider.value = 1f;
            audioSource.volume = 1f;
        }

        // Add listener to handle slider value changes
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    // Method to handle slider value changes
    private void OnVolumeChange(float value)
    {
        // Set the AudioSource volume and save the new volume setting
        audioSource.volume = value;
        PlayerPrefs.SetFloat("musicVolume", value);
    }
}
