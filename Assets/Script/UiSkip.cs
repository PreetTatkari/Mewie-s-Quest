using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video; // For VideoPlayer-based cutscenes

public class HideHPUIOnCutscene : MonoBehaviour
{
    public Slider[] hpSliders; // Array to hold multiple HP UI sliders
    public VideoPlayer cutscene; // Assign the VideoPlayer cutscene in Inspector

    private void Start()
    {
        if (cutscene != null)
        {
            cutscene.loopPointReached += OnCutsceneEnd;
            cutscene.started += OnCutsceneStart;
        }
    }

    private void OnCutsceneStart(VideoPlayer vp)
    {
        SetHPUIVisibility(false);
    }

    private void OnCutsceneEnd(VideoPlayer vp)
    {
        SetHPUIVisibility(true);
    }

    private void SetHPUIVisibility(bool isVisible)
    {
        foreach (Slider slider in hpSliders)
        {
            if (slider != null)
            {
                slider.gameObject.SetActive(isVisible);
            }
        }
    }

    private void OnDestroy()
    {
        if (cutscene != null)
        {
            cutscene.loopPointReached -= OnCutsceneEnd;
            cutscene.started -= OnCutsceneStart;
        }
    }
}
