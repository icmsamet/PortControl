using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public int MusicController = 1;
    public int EffectController = 1;
    [SerializeField] private AudioSource musicSource,soundEffectSource;

    [Header("MusicButtons")]
    [SerializeField] private GameObject onButtonMusic;
    [SerializeField] private GameObject offButtonMusic;

    [Header("SoundEffectButtons")]
    [SerializeField] private GameObject onButton;
    [SerializeField] private GameObject offButton;
    public void musicController(int i)
    {
        PlayerPrefs.SetInt("musicController", i);
    }

    public void soundEffectController(int i)
    {
        PlayerPrefs.SetInt("soundEffectController", i);
    }
    private void Update()
    {
        MusicController = PlayerPrefs.GetInt("musicController");
        EffectController = PlayerPrefs.GetInt("soundEffectController");
        if(MusicController == 1)
        {
            onButtonMusic.SetActive(true);
            offButtonMusic.SetActive(false);
            musicSource.mute = false;
            musicSource.gameObject.SetActive(true);
            musicSource.volume = 1;
        }
        else if(MusicController == 0)
        {
            onButtonMusic.SetActive(false);
            offButtonMusic.SetActive(true);
            musicSource.mute = true;
        }

        if (EffectController == 1)
        {
            onButton.SetActive(true);
            offButton.SetActive(false);
            soundEffectSource.mute = false;
            soundEffectSource.gameObject.SetActive(true);
            soundEffectSource.volume = 1;
        }
        else if (EffectController == 0)
        {
            onButton.SetActive(false);
            offButton.SetActive(true);
            soundEffectSource.mute = true;
        }
    }
}
