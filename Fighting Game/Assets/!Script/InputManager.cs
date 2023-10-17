using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
     public static InputManager instance;

    private PlayerInput playerInput;

    //booleans
    public bool moveLeftHeld { get; private set; }
    public bool moveLeftReleased { get; private set; }
    public bool moveRightHeld { get; private set; }
    public bool moveRightReleased { get; private set; }
    public bool JumpHeld { get; private set; }
    public bool JumpReleased { get; private set; }
    public bool Attack01Input { get; private set; }
    public bool Attack02Input { get; private set; }
    public bool GuardHeld { get; private set; }
    public bool GuardReleased { get; private set; }
    public bool MenuOpenCloseInput { get; private set; }


    private InputAction moveLeftAction;
    private InputAction moveRightAction;
    private InputAction JumpAction;
    private InputAction attack01Action;
    private InputAction attack02Action;
    private InputAction guardAction;
    private InputAction menuOpenCloseAction;


    private void Awake()
    {
        if (instance == null) { 
            instance = this;
        }

        playerInput = GetComponent<PlayerInput>();
        SetupInputActions();
    }

    private void Start()
    {
    }

    private void Update()
    {
        //Inputs
        UpdateInputs();
    }



    //Input Actions
    private void SetupInputActions()
    {
        moveLeftAction = playerInput.actions["Move Left"];
        moveRightAction = playerInput.actions["Move Right"];
        JumpAction = playerInput.actions["Jump"];
        attack01Action = playerInput.actions["Attack 1"];
        attack02Action = playerInput.actions["Attack 2"];
        guardAction = playerInput.actions["Guard"];
        menuOpenCloseAction = playerInput.actions["MenuOpenClose"];
    }

    private void UpdateInputs()
    {
        moveLeftHeld = moveLeftAction.IsPressed();
        moveLeftReleased = moveLeftAction.WasReleasedThisFrame();

        moveRightHeld = moveRightAction.IsPressed();
        moveRightReleased = moveRightAction.WasReleasedThisFrame();

        Attack01Input = attack01Action.WasPressedThisFrame();
        Attack02Input = attack02Action.WasPressedThisFrame();

        JumpHeld = JumpAction.IsPressed();
        JumpReleased = JumpAction.WasReleasedThisFrame();

        GuardHeld = guardAction.IsPressed();
        GuardReleased = guardAction.WasReleasedThisFrame();

        MenuOpenCloseInput = menuOpenCloseAction.WasPressedThisFrame();
    }
}
