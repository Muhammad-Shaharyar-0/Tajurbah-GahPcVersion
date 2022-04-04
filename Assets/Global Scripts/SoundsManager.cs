using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance = null;

    public bool soundOn = true;

    public AudioSource ButtonClickSound;
    public AudioSource SnapClickSound;
    public AudioSource BurnerSound;
    public AudioSource ClockSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    
    public void SwitchSoundOnOff()
    {
        soundOn = !soundOn;
    }

    public void PlayButtonClickSound()
    {
        if(soundOn)
        {
            ButtonClickSound.Play();
        }
    }

    public void PlaySnapClickSound()
    {
        if (soundOn)
        {
            SnapClickSound.Play();
        }
    }
    public void PlayBurnerSound()
    {
        if (soundOn)
        {
            BurnerSound.Play();
        }
    }
    public void StopBurnerSound()
    {
        BurnerSound.Stop();
    }

    public void PlayClockSound()
    {
        if (soundOn)
        {
            ClockSound.Play();
        }
    }
    public void StopClockSound()
    {
        ClockSound.Stop();
    }
}
