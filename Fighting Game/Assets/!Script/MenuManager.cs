using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Main Objects")]
    [SerializeField] private GameObject playerJoinedMenu;
    [SerializeField] private GameObject playerFightUI;

    [Header("Menu Objects")]
    [SerializeField] private GameObject player1MainMenu;
    [SerializeField] private GameObject player2MainMenu;

    [Header("Audio Menu Objects")]
    [SerializeField] private GameObject player1AudioMenu;
    [SerializeField] private GameObject player2AudioMenu;

    [Header("First Selection")]
    [SerializeField] private GameObject player1Button;
    [SerializeField] private GameObject player2Button;
    [SerializeField] private GameObject player1AudioButton;
    [SerializeField] private GameObject player2AudioButton;


    [Header("Audio")]
    [SerializeField] private Slider player1Volume;
    [SerializeField] private Slider player2Volume;
    [SerializeField] private AudioSource gameAudio;

    private bool player1SubMenuOpen;
    private bool player2SubMenuOpen;

    //During the Game
    void Start()
    {
        playerJoinedMenu.SetActive(true);

        player1MainMenu.SetActive(false);
        player2MainMenu.SetActive(false);

        player1AudioMenu.SetActive(false);
        player2AudioMenu.SetActive(false);

        player1SubMenuOpen = false;
        player2SubMenuOpen = false;

        //set audio bar
        player1Volume.value = gameAudio.volume;
        player2Volume.value = gameAudio.volume;
    }


    //Pause/Unpause
    public void Pause(int player) {
        if (player == 1) {
            player1MainMenu.SetActive(true);
            player2MainMenu.SetActive(false);

            EventSystem.current.SetSelectedGameObject(player1Button);
        }
        else if (player == 2) {
            player1MainMenu.SetActive(false);
            player2MainMenu.SetActive(true);

            EventSystem.current.SetSelectedGameObject(player2Button);
        }

        playerFightUI.SetActive(false);
    }

    public void Unpause(int player)
    {
        if (player == 1)
        {
            player1MainMenu.SetActive(false);
            player2MainMenu.SetActive(false);
        }
        else if (player == 2)
        {
            player1MainMenu.SetActive(false);
            player2MainMenu.SetActive(false);
        }

        playerFightUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
    }

    //audio settings
    //open
    public void player1AudioOpen() {
        player1MainMenu.SetActive(false);

        player1AudioMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(player1AudioButton);

        player1SubMenuOpen = true;
        player2SubMenuOpen = false;
    }

    public void player2AudioOpen() {
        player2MainMenu.SetActive(false);

        player2AudioMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(player2AudioButton);

        player1SubMenuOpen = false;
        player2SubMenuOpen = true;
    }

    //close
    public void player1AudioClose()
    {
        player1MainMenu.SetActive(true);

        player1AudioMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(player1Button);

        player1SubMenuOpen = false;
        player2SubMenuOpen = false;
    }

    public void player2AudioClose()
    {
        player2MainMenu.SetActive(true);

        player2AudioMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(player2Button);

        player1SubMenuOpen = false;
        player2SubMenuOpen = false;
    }

    //volumeControls
    public void player1VolumeControls() {
        float volume = player1Volume.value;

        gameAudio.volume = volume;
    }

    public void player2VolumeControls()
    {
        float volume = player2Volume.value;

        gameAudio.volume = volume;
    }

    //Settings Menu
    public void OnQuitPres() {
        SceneManager.LoadScene(0);
    }


    public bool isSubMenuOpen() {
        if (player1SubMenuOpen == true) {
            return true;
        }
        if (player2SubMenuOpen == true)
        {
            return true;
        }

        return false;
    }
}
