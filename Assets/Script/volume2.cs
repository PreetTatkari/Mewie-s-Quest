using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Sac : MonoBehaviour
{
    public AudioSource backgroundAudio; // The audio source for background music
    public VideoPlayer videoPlayer;     // The video player component
    public string selectedSceneName;    // The name of the scene where audio should play

    private void Start()
    {
        // Subscribe to the VideoPlayer events
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.started += OnVideoStarted;

        // Check if the current scene is the selected scene
        CheckSceneAudio();
    }

    private void Update()
    {
        // Continuously check the scene to ensure audio plays only in the selected scene
        CheckSceneAudio();
    }

    // This method checks if the audio should play in the current scene
    private void CheckSceneAudio()
    {
        // Get the active scene's name
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Play audio only if in the selected scene and video is not playing
        if (currentSceneName == selectedSceneName && !videoPlayer.isPlaying)
        {
            if (!backgroundAudio.isPlaying)
            {
                backgroundAudio.Play();
            }
        }
        else
        {
            backgroundAudio.Pause();
        }
    }

    // This method is triggered when the video starts playing
    private void OnVideoStarted(VideoPlayer vp)
    {
        backgroundAudio.Pause();
    }

    // This method is triggered when the video finishes playing
    private void OnVideoFinished(VideoPlayer vp)
    {
        CheckSceneAudio();
    }
}