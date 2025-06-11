using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneAudioController : MonoBehaviour
{
    public AudioSource backgroundAudio; // The audio source for background music
    public VideoPlayer videoPlayer1;    // The first video player component
    public VideoPlayer videoPlayer2;    // The second video player component
    public string selectedSceneName;    // The name of the scene where audio should play

    private void Start()
    {
        // Subscribe to the VideoPlayer events for both video players
        videoPlayer1.loopPointReached += OnVideoFinished;
        videoPlayer1.started += OnVideoStarted;

        videoPlayer2.loopPointReached += OnVideoFinished;
        videoPlayer2.started += OnVideoStarted;

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

        // Play audio only if in the selected scene and neither video is playing
        if (currentSceneName == selectedSceneName && !videoPlayer1.isPlaying && !videoPlayer2.isPlaying)
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

    // This method is triggered when either video starts playing
    private void OnVideoStarted(VideoPlayer vp)
    {
        backgroundAudio.Pause();
    }

    // This method is triggered when either video finishes playing
    private void OnVideoFinished(VideoPlayer vp)
    {
        // Check if both videos have finished before resuming audio
        if (!videoPlayer1.isPlaying && !videoPlayer2.isPlaying)
        {
            CheckSceneAudio();
        }
    }
}