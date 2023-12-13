using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenuSceneSelection : MonoBehaviour
{
    public PlayerInput inputSystem;
    public Image gameNextIcon;
    public Image gamePrevIcon;
    public Sprite[] NextIcons;
    public Sprite[] PrevIcons;

    public VideoPlayer videos;
    public VideoClip[] clips;

    public AudioSource audioSource;
    public AudioClip[] audioFile;

    public GameObject position1;
    public GameObject position2;

    public GameObject textObject;
    public GameObject titleTextObject;

    public GameObject nextFlash;
    public GameObject prevFlash;


    public AudioSource audioEffect;
    public AudioClip nextSoundEffect;
    public AudioClip prevSoundEffect;
    public AudioClip acptSoundEffect;

    bool nextFlasher = false;
    bool prevFlasher = false;
    bool acceptFlasher = false;
    bool acceptFirstFlash = true;

    float speed = 5;
    float blink = 1;
    float time = 0;

    public Color color1;

    public int sceneValue = 1;

    public GameObject AudioControls;

    public GameObject mainCanvas;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("completed") != 1) {
            PlayerPrefs.SetFloat("volume", 1);
            PlayerPrefs.SetFloat("SFX", 1);

            PlayerPrefs.SetInt("widdth", 1920);
            PlayerPrefs.SetInt("height", 1080);

            PlayerPrefs.SetInt("completed", 1);

            PlayerPrefs.SetInt("drop", 2);
        }

        audioSource.volume = PlayerPrefs.GetFloat("volume");
        audioEffect.volume = PlayerPrefs.GetFloat("SFX");

        if (PlayerPrefs.GetInt("drop") == 2)
        {
            Screen.SetResolution(PlayerPrefs.GetInt("width"), PlayerPrefs.GetInt("height"), Screen.fullScreen = true);
        }
        else
        {
            Screen.SetResolution(PlayerPrefs.GetInt("width"), PlayerPrefs.GetInt("height"), Screen.fullScreen = false);
        }

        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //game icons
        if (inputSystem.currentControlScheme == "Keyboard") {
            nextGameIcon();

        }
        else if (inputSystem.currentControlScheme == "Gamepad") {
            prevGameIcon();
        }


        //next prev flash
        if (nextFlasher == true) {
            nextFlash.GetComponent<Image>().color = Color.Lerp(Color.yellow, color1, Mathf.PingPong(Time.time * speed, 1));

            if (time < blink)
            {
                time += Time.deltaTime;
            }
            else {
                nextFlasher = false;

                nextFlash.GetComponent<Image>().color = color1;
            }
        }

        if (prevFlasher == true)
        {
            prevFlash.GetComponent<Image>().color = Color.Lerp(Color.yellow, color1, Mathf.PingPong(Time.time * speed, 1));

            if (time < blink)
            {
                time += Time.deltaTime;
            }
            else
            {
                prevFlasher = false;

                prevFlash.GetComponent<Image>().color = color1;
            }
        }



        //accpet flash
        if (acceptFlasher == true) {
            if (acceptFirstFlash == true) {
                videos.isLooping = false;
                videos.clip = clips[3];
                videos.playbackSpeed = 2;
                acceptFirstFlash = false;
            }

            if (time < blink)
            {
                time += Time.deltaTime;
            }
            else
            {
                if (sceneValue == 1)
                {
                    SceneManager.LoadScene(3);
                }
                else if (sceneValue == 2)
                {
                    SceneManager.LoadScene(2);
                }
                else if (sceneValue == 3)
                {
                    SceneManager.LoadScene(1);
                }
                else if (sceneValue == 4)
                {
                    Application.Quit();
                }
            }
        }
    }


    //Change Option
    public void OnNextOption() {
        if (acceptFlasher == false) {
            TextMeshProUGUI text = textObject.GetComponent<TextMeshProUGUI>();

            if (sceneValue == 1)
            {
                sceneValue = 2;

                videos.clip = clips[1];
                audioSource.clip = audioFile[1];
                audioSource.Play();

                text.text = "Practice Mode";
                text.transform.position = position1.transform.position;
            }
            else if (sceneValue == 2)
            {
                sceneValue = 3;

                videos.clip = clips[2];
                audioSource.clip = audioFile[2];
                audioSource.Play();

                text.text = "Settings";
                text.transform.position = position2.transform.position;
            }
            else if (sceneValue == 3)
            {
                sceneValue = 4;

                videos.clip = clips[2];
                audioSource.clip = audioFile[2];
                audioSource.Play();

                text.text = "Quit Game";
                text.transform.position = position2.transform.position;

            } else if (sceneValue == 4) {
                sceneValue = 1;

                videos.clip = clips[0];
                audioSource.clip = audioFile[0];
                audioSource.Play();

                text.text = "Local Multiplayer";
                text.transform.position = position1.transform.position;
            }

            time = 0;
            nextFlasher = true;
            prevFlasher = false;

            prevFlash.GetComponent<Image>().color = color1;

            audioEffect.clip = nextSoundEffect;
            audioEffect.Play();
        }
    }


    public void OnPrevOption() {
        if (acceptFlasher == false)
        {
            TextMeshProUGUI text = textObject.GetComponent<TextMeshProUGUI>();

            if (sceneValue == 1)
            {
                sceneValue = 4;

                videos.clip = clips[2];
                audioSource.clip = audioFile[2];
                audioSource.Play();

                text.text = "Quit Game";
                text.transform.position = position2.transform.position;
            }
            else if (sceneValue == 2)
            {
                sceneValue = 1;

                videos.clip = clips[0];
                audioSource.clip = audioFile[0];
                audioSource.Play();

                text.text = "Local Multiplayer";
                text.transform.position = position1.transform.position;
            }
            else if (sceneValue == 3)
            {
                sceneValue = 2;

                videos.clip = clips[1];
                audioSource.clip = audioFile[1];
                audioSource.Play();

                text.text = "Practice Mode";
                text.transform.position = position1.transform.position;
            }
            else if (sceneValue == 4)
            {
                sceneValue = 3;

                videos.clip = clips[2];
                audioSource.clip = audioFile[2];
                audioSource.Play();

                text.text = "Settings";
                text.transform.position = position2.transform.position;
            }

            time = 0;
            nextFlasher = false;
            prevFlasher = true;

            nextFlash.GetComponent<Image>().color = color1;

            audioEffect.clip = prevSoundEffect;
            audioEffect.Play();
        }
    }

    public void OnAccept() {
        if (sceneValue != 5) {
            time = 0;
            blink = 2.5f;

            nextFlasher = false;
            prevFlasher = false;

            nextFlash.GetComponent<Image>().color = color1;
            prevFlash.GetComponent<Image>().color = color1;

            acceptFlasher = true;

            audioEffect.clip = acptSoundEffect;
            audioEffect.Play();

            Destroy(textObject);
            Destroy(titleTextObject);
        }
    }

    //game icons
    private void nextGameIcon() {
        gameNextIcon.sprite = NextIcons[0];
        gamePrevIcon.sprite = PrevIcons[0];
    }

    private void prevGameIcon()
    {
        gamePrevIcon.sprite = PrevIcons[1];
        gameNextIcon.sprite = NextIcons[1];
    }
}
