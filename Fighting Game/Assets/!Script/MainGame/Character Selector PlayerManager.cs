using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSelectorPlayerManager : MonoBehaviour
{
    public GameObject plyerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInputManager.instance.playerPrefab = plyerPrefab;
        //PlayerInputManager.instance.JoinPlayer(0, -1, null);
        PlayerInputManager.instance.JoinPlayer(1, -1, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
