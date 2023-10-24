using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SceneSelection : MonoBehaviour
{
    public PlayerInput inputSystem;
    public Image gameNextIcon;
    public Image gamePrevIcon;
    public Sprite[] NextIcons;
    public Sprite[] PrevIcons;

    public GameObject stage;

    public GameObject textObject;
    public GameObject titleText;


    public GameObject[] scenes;
    public int selected = 0;

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

    public AudioSource audioVolume;

    void Start()
    {
        audioVolume.volume = PlayerPrefs.GetFloat("volume");
        audioEffect.volume = PlayerPrefs.GetFloat("SFX");

        GetSprite();
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
                PlayerPrefs.SetInt("StageSelect", selected);

                SceneManager.LoadScene(4);
            }
        }
    }


    public void OnNextOption() {
        if (acceptFlasher == false)
        {
            TextMeshProUGUI text = textObject.GetComponent<TextMeshProUGUI>();

            if (selected == 0)
            {
                selected = 1;

                text.text = "Ancient Ruins";
            }
            else if (selected == 1)
            {
                selected = 2;

                text.text = "Sky Garden";
            }
            else if (selected == 2)
            {
                selected = 3;

                text.text = "Ancient Shrine";
            }
            else if (selected == 3)
            {
                selected = 0;

                text.text = "The Alpines";
            }

            time = 0;
            nextFlasher = true;
            prevFlasher = false;

            prevFlash.GetComponent<Image>().color = color1;

            audioEffect.clip = nextSoundEffect;
            audioEffect.Play();

            GetSprite();
        }
    }


    public void OnPrevOption() {
        if (acceptFlasher == false)
        {
            TextMeshProUGUI text = textObject.GetComponent<TextMeshProUGUI>();

            if (selected == 0)
            {
                selected = 3;

                text.text = "Ancient Shrine";
            }
            else if (selected == 1)
            {
                selected = 0;

                text.text = "The Alpines";
            }
            else if (selected == 2)
            {
                selected = 1;

                text.text = "Ancient Ruins";
            }
            else if (selected == 3)
            {
                selected = 2;

                text.text = "Sky Garden";
            }

            time = 0;
            nextFlasher = false;
            prevFlasher = true;

            nextFlash.GetComponent<Image>().color = color1;

            audioEffect.clip = prevSoundEffect;
            audioEffect.Play();

            GetSprite();
        }
    }

    public void GetSprite() {
        stage.GetComponent<Image>().sprite = scenes[selected].GetComponent<SpriteRenderer>().sprite;
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
        Destroy(stage);
    }

    //OnBack
    public void OnBack() {
        SceneManager.LoadScene(2);
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
