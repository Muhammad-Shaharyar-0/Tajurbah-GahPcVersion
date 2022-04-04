using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SideMenuController : MonoBehaviour
{
    public static SideMenuController Instance=null;

    [SerializeField] GameObject sideMenuOverlay;

    [SerializeField] Text SoundButtonText;
    float currentTimeScale = 1;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
    
        sideMenuOverlay.SetActive(false);
    }

    public void SideMenuButton()
    {
        sideMenuOverlay.SetActive(true);
        currentTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void CrossMenuButton()
    {
        sideMenuOverlay.SetActive(false);
        Time.timeScale = currentTimeScale;
    }
    public void SoundButtons()
    {
      
        if (SoundsManager.Instance.soundOn)
        {
          
            SoundButtonText.text = "Sound Off";
        }
        
        else
        {
            SoundButtonText.text = "Sound On";
        }
        SoundsManager.Instance.SwitchSoundOnOff();

    }

    public void PlayClickSound()
    {
        SoundsManager.Instance.PlayButtonClickSound();
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
