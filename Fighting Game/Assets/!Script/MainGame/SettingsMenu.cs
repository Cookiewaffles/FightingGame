using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Canvas Objects")]
    public GameObject mainCanvas;
    public GameObject VideoCanvas;
    public GameObject AudioCanvas;

    [Header("Text Menus")]
    public TextMeshProUGUI video;
    public TextMeshProUGUI audio;
    public TextMeshProUGUI exit;
    public Color activeColor;
    public Color deactiveColor;
    public Color selectedColor;

    [Header("First Selected")]
    public GameObject videoSelection;
    public GameObject audioSelection;

    [Header("Audio Items")]
    public AudioClip nextSoundEffect;
    public AudioClip prevSoundEffect;
    public AudioClip acptSoundEffect;

    [Header("Audio Menu")]
    public AudioSource audioSource;
    public AudioSource audioEffect;
    public Slider audioController;
    public Slider SFXAudio;

    [Header("Video Menu")]
    public TMP_Dropdown dropdownMenu;



    bool submenu;
    int menuValue;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("completed") != 1)
        {
            PlayerPrefs.SetFloat("volume", 1);
            PlayerPrefs.SetFloat("SFX", 1);

            PlayerPrefs.SetInt("completed", 1);
        }

        audioController.value = PlayerPrefs.GetFloat("volume");
        SFXAudio.value = PlayerPrefs.GetFloat("SFX");

        submenu = false;
        menuValue = 0;

        VideoCanvas.SetActive(true);
        AudioCanvas.SetActive(false);

        dropdownMenu.value = PlayerPrefs.GetInt("drop"); ;
    }

    //on left
    public void OnLeft() {
        if (submenu == false) {
            if (menuValue == 0) {
                menuValue = 2;

                video.color = deactiveColor;
                audio.color = deactiveColor;
                exit.color = activeColor;

                video.fontStyle = FontStyles.Normal;
                exit.fontStyle = FontStyles.Underline;

                VideoCanvas.SetActive(true);
                AudioCanvas.SetActive(false);
            } else if (menuValue == 1) {
                menuValue = 0;

                video.color = activeColor;
                audio.color = deactiveColor;
                exit.color = deactiveColor;

                video.fontStyle = FontStyles.Underline;
                audio.fontStyle = FontStyles.Normal;

                VideoCanvas.SetActive(true);
                AudioCanvas.SetActive(false);
            } else if (menuValue == 2) {
                menuValue = 1;

                video.color = deactiveColor;
                audio.color = activeColor;
                exit.color = deactiveColor;

                audio.fontStyle = FontStyles.Underline;
                exit.fontStyle = FontStyles.Normal;

                VideoCanvas.SetActive(false);
                AudioCanvas.SetActive(true);
            }
        }
    }

    //on right
    public void OnRight() {
        if (submenu == false)
        {
            if (menuValue == 0)
            {
                menuValue = 1;

                video.color = deactiveColor;
                audio.color = activeColor;
                exit.color = deactiveColor;

                video.fontStyle = FontStyles.Normal;
                audio.fontStyle = FontStyles.Underline;

                VideoCanvas.SetActive(false);
                AudioCanvas.SetActive(true);
            }
            else if (menuValue == 1)
            {
                menuValue = 2;

                video.color = deactiveColor;
                audio.color = deactiveColor;
                exit.color = activeColor;

                exit.fontStyle = FontStyles.Underline;
                audio.fontStyle = FontStyles.Normal;

                VideoCanvas.SetActive(false);
                AudioCanvas.SetActive(true);
            }
            else if (menuValue == 2)
            {
                menuValue = 0;

                video.color = activeColor;
                audio.color = deactiveColor;
                exit.color = deactiveColor;

                video.fontStyle = FontStyles.Underline;
                exit.fontStyle = FontStyles.Normal;

                VideoCanvas.SetActive(true);
                AudioCanvas.SetActive(false);
            }
        }
    }

    //on accept
    public void OnAccept() {
        if (submenu == false) {
            submenu = true;

            audioEffect.clip = acptSoundEffect;
            audioEffect.Play();

            if (menuValue == 0) {
                EventSystem.current.SetSelectedGameObject(videoSelection);

                video.color = selectedColor;
            }
            else if (menuValue == 1){
                EventSystem.current.SetSelectedGameObject(audioSelection);

                audio.color = selectedColor;
            }
            else if (menuValue == 2)
            {
                SceneManager.LoadScene(0);

                exit.color = selectedColor;
            }
        }
    }

    //on back
    public void OnBack() {
        if (submenu == true) {
            submenu = false;

            EventSystem.current.SetSelectedGameObject(null);

            video.color = deactiveColor;
            audio.color = deactiveColor;
            exit.color = deactiveColor;

            if (menuValue == 0) {
                video.color = activeColor;
            }
            else if (menuValue == 1) {
                audio.color = activeColor;
            }
            else if (menuValue == 2)
            {
                exit.color = activeColor;
            }

        }
        else if (submenu == false) { 
            SceneManager.LoadScene(0);
        }
    }


    //audio stuff
    public void volumeUpdate()
    {
        float volume = audioController.value;

        audioSource.volume = volume;

        PlayerPrefs.SetFloat("volume", volume);
    }

    public void SFXUpdate()
    {
        float SFX = SFXAudio.value;

        audioEffect.volume = SFX;

        PlayerPrefs.SetFloat("SFX", SFX);
    }


    //video stuff
    public void resolutionOptions() {
        if (dropdownMenu.value == 0) {
            PlayerPrefs.SetInt("width", 640);
            PlayerPrefs.SetInt("height", 480);

            PlayerPrefs.SetInt("drop", 0);
        }
        else if (dropdownMenu.value == 1) {
            PlayerPrefs.SetInt("width", 1280);
            PlayerPrefs.SetInt("height", 720);

            PlayerPrefs.SetInt("drop", 1);
        }
        else if (dropdownMenu.value == 2) {
            PlayerPrefs.SetInt("width", 1920);
            PlayerPrefs.SetInt("height", 1080);

            PlayerPrefs.SetInt("drop", 2);
        }

        setReseloution();
    }

    public void setReseloution()
    {
        if (PlayerPrefs.GetInt("drop") == 2) {
            Screen.SetResolution(PlayerPrefs.GetInt("width"), PlayerPrefs.GetInt("height"), Screen.fullScreen = true);
        }
        else
        {
            Screen.SetResolution(PlayerPrefs.GetInt("width"), PlayerPrefs.GetInt("height"), Screen.fullScreen = false);
        }
    }
}
