using UnityEngine;
using UnityEngine.UI;  
using UnityEngine.Video;  
using System.Collections.Generic;  

public class YarnballEnemy : MonoBehaviour
{
    public int maxHP = 50;                
    private int currentHP;                

    public string primaryProjectileTag = "PrimaryProjectile";  
    public string secondaryProjectileTag = "SecondaryProjectile";  

    public int primaryProjectileDamage = 25;  
    public int secondaryProjectileDamage = 10;  

    public VideoPlayer videoPlayer;       
    public Text hpText;                   
    public Slider hpSlider;               
    public List<GameObject> uiElements;  

    private void Start()
    {
        currentHP = maxHP;  
        UpdateHPUI();       
        videoPlayer.loopPointReached += ResumeGameAfterVideo;  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(primaryProjectileTag))
        {
            TakeDamage(primaryProjectileDamage);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag(secondaryProjectileTag))
        {
            TakeDamage(secondaryProjectileDamage);
            Destroy(other.gameObject);
        }
    }

    private void TakeDamage(int damageAmount)
    {
        currentHP -= damageAmount;
        UpdateHPUI();

        Debug.Log("Yarnball HP: " + currentHP);

        if (currentHP <= 0)
        {
            currentHP = 0;
            Debug.Log("Yarnball Destroyed");
            DestroyYarnball();
        }
    }

    private void DestroyYarnball()
    {
        SetUIActive(false);
        if (hpSlider != null) hpSlider.gameObject.SetActive(false);

        PauseGameAndPlayVideo();
        Destroy(gameObject);
    }

    private void PauseGameAndPlayVideo()
    {
        Time.timeScale = 0f;  
        videoPlayer.gameObject.SetActive(true);  
        videoPlayer.Play();  

        SetUIActive(false);
    }

    private void ResumeGameAfterVideo(VideoPlayer vp)
    {
        Time.timeScale = 1f;  
        videoPlayer.gameObject.SetActive(false);  
    }

    private void SetUIActive(bool active)
    {
        foreach (GameObject uiElement in uiElements)
        {
            uiElement.SetActive(active);
        }
    }

    private void UpdateHPUI()
    {
        if (hpText != null)
        {
            hpText.text = "Yarnball HP: " + currentHP;
        }
        if (hpSlider != null)
        {
            hpSlider.value = (float)currentHP / maxHP;

            if (currentHP <= 0)
            {
                hpSlider.gameObject.SetActive(false);
            }
        }
    }
}
