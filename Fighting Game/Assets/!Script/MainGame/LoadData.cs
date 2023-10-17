using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LoadData : MonoBehaviour
{
    public GameObject[] player1CharacterPrefab;
    public GameObject[] player2CharacterPrefab;
    public GameObject[] stagePrefab;
    public Sprite[] CharacterPics;

    public GameObject player1Pic;
    public GameObject player2Pic;

    GameObject player1Prefab;
    GameObject player2Prefab;

    public Transform player1Spawn;
    public Transform player2Spawn;
    public Transform stageLoad;

    public AudioSource audio;


    int stageSelected;
    int player1Selected;
    int player2Selected;
    int bestof;

    public GameObject gauge1;
    public GameObject guage2;

    public TMP_Text player1;
    public TMP_Text player2;

    public static LoadData instance = null;

    public InputAction joinAction;
    public InputAction leaveAction;

    int players = 0;

    bool player1text = false;
    bool player2text = false;
    bool binding = false;

    float speed = 5;
    float blink = 5;
    float time = 0;

    private void Awake()
    {
        leaveAction.Enable();
        leaveAction.performed += context => LeaveAction(context);

        joinAction.Enable();
        joinAction.performed += context => JoinAction(context);
    }

    // Start is called before the first frame update
    void Start() 
    {
        audio.Stop();

        //Stage Load
        stageSelected = PlayerPrefs.GetInt("StageSelect");
        GameObject stageSelectedPrefab = stagePrefab[stageSelected];
        GameObject stageSelectedClone = Instantiate(stageSelectedPrefab, stageLoad.position, Quaternion.identity);

        //player 1 Load
        player1Selected = PlayerPrefs.GetInt("player1");
        player1Prefab = player1CharacterPrefab[player1Selected];
        player1Pic.GetComponent<Image>().sprite = CharacterPics[player1Selected];
        player1Prefab.name = "Player 1";

        //player 2 Load
        player2Selected = PlayerPrefs.GetInt("player2");
        player2Prefab = player2CharacterPrefab[player2Selected];
        player2Pic.GetComponent<Image>().sprite = CharacterPics[player2Selected];
        player2Prefab.name = "Player 2";

        //ruleset
        bestof = PlayerPrefs.GetInt("bestof");

        if (bestof == 1) { 
            gauge1.SetActive(false);
            guage2.SetActive(false);
        }
    }

    public void Update()
    {
        if (player1text == true && player2text == true && binding == false) {
            if (time < blink) {
                time += Time.deltaTime;
            }
            else {
                Time.timeScale = 1f;

                GameObject playertext = GameObject.Find("PlayersJoined");
                Destroy(playertext);

                Player1_Moves player1Moves = GameObject.Find("Player 1(Clone)").GetComponent<Player1_Moves>();
                player1Moves.BindObjects();

                Player1VictoryCondtions player1Vic = GameObject.Find("Player 1(Clone)").GetComponent<Player1VictoryCondtions>();
                player1Vic.BindObjects();

                Player2_Moves player2Moves = GameObject.Find("Player 2(Clone)").GetComponent<Player2_Moves>();
                player2Moves.BindObjects();

                Player2VictoryConditions player2Vic = GameObject.Find("Player 2(Clone)").GetComponent<Player2VictoryConditions>();
                player2Vic.BindObjects();


                GameObject player1 = GameObject.Find("Player 1(Clone)");
                player1.transform.position = player1Spawn.position;

                GameObject player2 = GameObject.Find("Player 2(Clone)");
                player2.transform.position = player2Spawn.position;


                audio.Play();

                binding = true;
            }
        }
    }

    void JoinAction(InputAction.CallbackContext context) {
        if (players == 0) {
            PlayerInputManager.instance.playerPrefab = player1Prefab;
            PlayerInputManager.instance.JoinPlayerFromActionIfNotAlreadyJoined(context);

        }
        else if(players == 1){
            PlayerInputManager.instance.playerPrefab = player2Prefab;
            PlayerInputManager.instance.JoinPlayerFromActionIfNotAlreadyJoined(context);

        }

        players = players + 1;
    }

    void LeaveAction(InputAction.CallbackContext context) { 
    
    }

    public void createPlayer() {
        if (players == 0)
        {
            player1.text = "Player 1 Joined";
            player1text = true;
        }
        else if (players == 1)
        {
            player2.text = "Player 2 Joined";
            player2text = true;
        }
    }
}
