using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject mainSettingsPanel; // Assign in Inspector
    public GameObject volumePanel;       // Assign in Inspector
    public Slider volumeSlider;          // Assign in Inspector

    private AudioSource audioSource;     // Reference to an AudioSource

    void Start()
    {
        // Initialize audio source if needed
        audioSource = FindObjectOfType<AudioSource>();

        // Set up slider value from PlayerPrefs or default
        if (audioSource != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
            audioSource.volume = volumeSlider.value;
        }

        // Set the initial panel
        ShowMainSettingsPanel();
    }

    public void ShowMainSettingsPanel()
    {
        mainSettingsPanel.SetActive(true);
        volumePanel.SetActive(false);
    }

    public void ShowVolumePanel()
    {
        mainSettingsPanel.SetActive(false);
        volumePanel.SetActive(true);
    }

    public void BackToMainSettings()
    {
        ShowMainSettingsPanel();
    }

    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
            PlayerPrefs.SetFloat("Volume", volume);
        }
    }
}
