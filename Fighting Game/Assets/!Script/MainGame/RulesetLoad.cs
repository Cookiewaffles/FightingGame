using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class RulesetLoad : MonoBehaviour
{
    public PlayerInput inputSystem;
    public Image gameNextIcon;
    public Image gamePrevIcon;
    public Sprite[] NextIcons;
    public Sprite[] PrevIcons;

    public GameObject textObject;
    public GameObject titleText;

    public GameObject nextFlash;
    public GameObject prevFlash;

    public AudioSource audioEffect;
    public AudioClip nextSoundEffect;
    public AudioClip prevSoundEffect;
    public AudioClip acptSoundEffect;

    public VideoPlayer videos;
    public VideoClip clips;

    bool nextFlasher = false;
    bool prevFlasher = false;
    bool acceptFlasher = false;
    bool acceptFirstFlash = true;

    float speed = 5;
    float blink = 1;
    float time = 0;

    public Color color1;

    public int selected = 1;

    public AudioSource audioVolume;

    private void Start()
    {
        audioVolume.volume = PlayerPrefs.GetFloat("volume");
        audioEffect.volume = PlayerPrefs.GetFloat("SFX");
    }

    // Update is called once per frame
    void Update()
    {
        //game icons
        if (inputSystem.currentControlScheme == "Keyboard")
        {
            nextGameIcon();

        }
        else if (inputSystem.currentControlScheme == "Gamepad")
        {
            prevGameIcon();
        }


        //next prev flash
        if (nextFlasher == true)
        {
            nextFlash.GetComponent<Image>().color = Color.Lerp(Color.yellow, color1, Mathf.PingPong(Time.time * speed, 1));

            if (time < blink)
            {
                time += Time.deltaTime;
            }
            else
            {
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
        if (acceptFlasher == true)
        {
            if (acceptFirstFlash == true)
            {
                videos.isLooping = false;
                videos.clip = clips;
                videos.playbackSpeed = 2;
                acceptFirstFlash = false;
            }

            if (time < blink)
            {
                time += Time.deltaTime;
            }
            else
            {
                PlayerPrefs.SetInt("bestof", selected);

                SceneManager.LoadScene(5);
            }
        }
    }


    public void OnNextOption()
    {
        if (acceptFlasher == false)
        {
            TextMeshProUGUI text = textObject.GetComponent<TextMeshProUGUI>();

            if (selected == 1)
            {
                selected = 2;

                text.text = "Best  of  3";
            }
            else if (selected == 2)
            {
                selected = 1;

                text.text = "Best  of  1";
            }


            time = 0;
            nextFlasher = true;
            prevFlasher = false;

            prevFlash.GetComponent<Image>().color = color1;

            audioEffect.clip = nextSoundEffect;
            audioEffect.Play();
        }
    }


    public void OnPrevOption()
    {
        if (acceptFlasher == false)
        {
            TextMeshProUGUI text = textObject.GetComponent<TextMeshProUGUI>();

            if (selected == 1)
            {
                selected = 2;

                text.text = "Best  of  3";
            }
            else if (selected == 2)
            {
                selected = 1;

                text.text = "Best  Of  1";
            }


            time = 0;
            nextFlasher = false;
            prevFlasher = true;

            nextFlash.GetComponent<Image>().color = color1;

            audioEffect.clip = nextSoundEffect;
            audioEffect.Play();
        }
    }

        //accept cause
    public void OnAccept()
    {
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
        Destroy(titleText);
    }

    //game icons
    private void nextGameIcon()
    {
        gameNextIcon.sprite = NextIcons[0];
        gamePrevIcon.sprite = PrevIcons[0];
    }

    private void prevGameIcon()
    {
        gamePrevIcon.sprite = PrevIcons[1];
        gameNextIcon.sprite = NextIcons[1];
    }
}
