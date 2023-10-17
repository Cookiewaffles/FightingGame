using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuInput : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private GameObject mainMenuCanvasGO;

    [Header("First Selection")]

    private bool isPaused = false;
    //During the Game
    void Start()
    {
        mainMenuCanvasGO.SetActive(false);
    }

    public void isPausedOn()
    {
        if (isPaused == false)
        {
            Pause();
        }
        else
        {
            Unpause();
        }
    }


    private void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;

        OpenMainMenu();
    }

    private void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1.0f;

        CloseMainMenu();
    }


    private void OpenMainMenu()
    {
        mainMenuCanvasGO.SetActive(true);
    }

    private void CloseMainMenu()
    {
        mainMenuCanvasGO.SetActive(false);
    }
}
