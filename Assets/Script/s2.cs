using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;  
using System.Collections.Generic;  

public class GameStartCutscene : MonoBehaviour
{
    public VideoPlayer videoPlayer;  
    public List<GameObject> uiElements;  
    public Slider hpSlider1;  // First HP slider
    public Slider hpSlider2;  // Second HP slider

    private void Start()
    {
        PauseGameAndPlayCutscene();
        videoPlayer.loopPointReached += ResumeGameAfterCutscene;
    }

    private void PauseGameAndPlayCutscene()
    {
        Time.timeScale = 0f;  
        videoPlayer.gameObject.SetActive(true);  
        videoPlayer.Play();  

        SetUIActive(false);
        SetHPUIActive(false);  // Hide HP sliders during the cutscene
    }

    private void ResumeGameAfterCutscene(VideoPlayer vp)
    {
        Time.timeScale = 1f;  
        videoPlayer.gameObject.SetActive(false);  

        SetUIActive(true);
        SetHPUIActive(true);  // Show HP sliders after cutscene
    }

    private void SetUIActive(bool active)
    {
        foreach (GameObject uiElement in uiElements)
        {
            uiElement.SetActive(active);
        }
    }

    private void SetHPUIActive(bool active)
    {
        if (hpSlider1 != null) hpSlider1.gameObject.SetActive(active);
        if (hpSlider2 != null) hpSlider2.gameObject.SetActive(active);
    }
}
