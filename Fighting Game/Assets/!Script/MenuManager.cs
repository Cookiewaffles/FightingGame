using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private GameObject player1MainMenu;
    [SerializeField] private GameObject player2MainMenu;

    //During the Game
    void Start()
    {
        player1MainMenu.SetActive(false);
        player2MainMenu.SetActive(false);
    }


    //Pause/Unpause
    public void Pause(int player) {
        if (player == 1) {
            

            player1MainMenu.SetActive(true);
            player2MainMenu.SetActive(false);
        }
        else if (player == 2) {
            player1MainMenu.SetActive(false);
            player2MainMenu.SetActive(true);
        }
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
    }

    //Open/Close Menu
    private void OpenMainMenu() {
        
    }

    private void OpenControlSettingsMenu() {
        
    }

    private void CloseMainMenu()
    {
        
    }

    //Settings Menu
    public void OnQuitPres() {
        SceneManager.LoadScene(0);
    }
}
