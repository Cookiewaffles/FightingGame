using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using static Unity.Collections.AllocatorManager;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] characters;
    public int selected = 0;
    public int playersReady = 0;

    public PlayerInput inputSystem;
    public PlayerInput player2;

    public Image gameNextIcon;
    public Image gamePrevIcon;
    public Sprite[] NextIcons;
    public Sprite[] PrevIcons;

    public Sprite[] playerIcons;
    public GameObject player;

    public GameObject textObject;
    public GameObject titleTextObject;


    public GameObject nextFlash;
    public GameObject prevFlash;


    public AudioSource audioEffect;
    public AudioClip nextSoundEffect;
    public AudioClip prevSoundEffect;
    public AudioClip acptSoundEffect;

    public VideoPlayer videos;
    public VideoClip clips;

    public GameObject background;
    public GameObject chars;

    bool nextFlasher = false;
    bool prevFlasher = false;
    bool acceptFlasher = false;
    bool acceptFirstFlash = true;

    float speed = 5;
    float blink = 1;
    float time = 0;

    public UnityEngine.Color color1;

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
            nextFlash.GetComponent<Image>().color = UnityEngine.Color.Lerp(UnityEngine.Color.yellow, color1, Mathf.PingPong(Time.time * speed, 1));

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
            prevFlash.GetComponent<Image>().color = UnityEngine.Color.Lerp(UnityEngine.Color.yellow, color1, Mathf.PingPong(Time.time * speed, 1));

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


        if (acceptFlasher == true) {
            if (acceptFirstFlash == true)
            {
                videos.isLooping = false;
                videos.clip = clips;
                videos.playbackSpeed = 2;
                videos.Play();
                acceptFirstFlash = false;
            }

            if (time < blink)
            {
                time += Time.deltaTime;
            }
            else
            {

                SceneManager.LoadScene(3);
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

                characters[1].SetActive(true);
                characters[0].SetActive(false);

                player.GetComponent<Image>().sprite = playerIcons[1];

                text.text = "Man 2";
            }
            else if (selected == 1)
            {
                selected = 2;

                player.GetComponent<Image>().sprite = playerIcons[2];

                characters[2].SetActive(true);
                characters[1].SetActive(false);

                text.text = "Man 3";
            }
            else if (selected == 2)
            {
                selected = 3;

                player.GetComponent<Image>().sprite = playerIcons[3];

                characters[3].SetActive(true);
                characters[2].SetActive(false);

                text.text = "Man 4";
            }
            else if (selected == 3)
            {
                selected = 4;

                player.GetComponent<Image>().sprite = playerIcons[4];

                characters[4].SetActive(true);
                characters[3].SetActive(false);

                text.text = "Man 5";
            }
            else if (selected == 4)
            {
                selected = 0;

                player.GetComponent<Image>().sprite = playerIcons[0];

                characters[0].SetActive(true);
                characters[4].SetActive(false);

                text.text = "Man 1";
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

            if (selected == 0)
            {
                selected = 4;

                player.GetComponent<Image>().sprite = playerIcons[4];

                characters[4].SetActive(true);
                characters[0].SetActive(false);

                text.text = "Man 5";
            }
            else if (selected == 1)
            {
                selected = 0;

                player.GetComponent<Image>().sprite = playerIcons[0];

                characters[0].SetActive(true);
                characters[1].SetActive(false);

                text.text = "Man 1";
            }
            else if (selected == 2)
            {
                selected = 1;

                player.GetComponent<Image>().sprite = playerIcons[1];

                characters[1].SetActive(true);
                characters[2].SetActive(false);

                text.text = "Man 2";
            }
            else if (selected == 3)
            {
                selected = 2;

                player.GetComponent<Image>().sprite = playerIcons[2];

                characters[2].SetActive(true);
                characters[3].SetActive(false);

                text.text = "Man 3";
            }
            else if (selected == 4)
            {
                selected = 3;

                player.GetComponent<Image>().sprite = playerIcons[3];

                characters[3].SetActive(true);
                characters[4].SetActive(false);

                text.text = "Man 4";
            }

            time = 0;
            nextFlasher = false;
            prevFlasher = true;

            nextFlash.GetComponent<Image>().color = color1;

            audioEffect.clip = prevSoundEffect;
            audioEffect.Play();
        }
    }


    //accept cause
    public void OnAccept()
    {
        if (playersReady == 0) {
            nextFlasher = false;
            prevFlasher = false;

            nextFlash.GetComponent<Image>().color = color1;
            prevFlash.GetComponent<Image>().color = color1;

            audioEffect.clip = acptSoundEffect;
            audioEffect.Play();

            TextMeshProUGUI text = titleTextObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI text2 = textObject.GetComponent<TextMeshProUGUI>();

            text.text = "Player 2 Selection";

            PlayerPrefs.SetInt("player1", selected);


            selected = 0;

            player.GetComponent<Image>().sprite = playerIcons[4];

            characters[0].SetActive(true);
            characters[1].SetActive(false);
            characters[2].SetActive(false);
            characters[3].SetActive(false);
            characters[4].SetActive(false);

            player.GetComponent<Image>().sprite = playerIcons[0];

            text2.text = "Man 1";


            playersReady = 1;

        } else if (playersReady == 1)
        {
            time = 0;
            blink = 2.5f;

            nextFlasher = false;
            prevFlasher = false;

            nextFlash.GetComponent<Image>().color = color1;
            prevFlash.GetComponent<Image>().color = color1;

            audioEffect.clip = acptSoundEffect;
            audioEffect.Play();

            acceptFlasher = true;

            PlayerPrefs.SetInt("player2", selected);

            playersReady = 2;

            Destroy(background);
            Destroy(player);
            Destroy(chars);
            Destroy(titleTextObject);
            Destroy(textObject);
        }
    }

    //OnBack
    public void OnBack()
    {
        SceneManager.LoadScene(0);
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
