using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player1_Moves : MonoBehaviour
{
    
    //animation variable
    public GameObject character;
    public GameObject enemy;
    public GameObject weapon;


    //Player Keybinds
    Animator controller;
    Animator enemyController;

    public bool isBlocking;
    public bool isMoving;

    //movement variable
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float horizontal;
    private float speed;
    private Vector2 moveInputValue; 
    private float jumpingPower;
    private bool isFacingRight;


    //Health Variables
    public Image heathBar;

    [Range(0, 100f)]
    public float playerHP;
    float playerHPValue;

    public float lightattackDmg;
    public float strongAttackDmg;
    public float blockingOffset;
    public bool isHit;
    public bool isBlockingEnemy;


    //stamina Variables
    public Image staminaBar;

    [Range(0, 100f)]
    public float playerStamina;
    float playerStaminaValue;

    //Win Guage Variables
    public Image gauge1;
    public bool first;

    //pause
    bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        FalseBind();


        controller = character.GetComponent<Animator>();
        enemyController = enemy.GetComponent<Animator>();

        isBlockingEnemy = false;
        isBlocking = false;
        isMoving = false;

        speed = 8f;
        jumpingPower = 16f;
        isFacingRight = true;

        playerHP = 100f;
        playerStamina = 100f;
        isHit = false;
        first = false;
    }

    // Update is called once per frame
    void Update()
    {

        //Player stats
        playerHPValue = playerHP * .01f;
        heathBar.fillAmount = playerHPValue;

        playerStaminaValue = playerStamina * .01f;
        staminaBar.fillAmount = playerStaminaValue;

        isBlockingEnemy = enemyController.GetBool("IsBlocking");

        flip();

        //Stamina Recharge
        if (playerStamina < 100f)
        {
            playerStamina = playerStamina + (5 * Time.deltaTime);
        }

        if (playerHP == 0f && gauge1.color == Color.red && first == false)
        {
            playerHP = 100f;
            first = true;
        }
    }

    //Physics
    public void flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 local = transform.localScale;
            local.x *= -1f;
            transform.localScale = local;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }




    //Collision Data
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == enemy.gameObject.name)
        {
            isHit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == enemy.gameObject.name)
        {
            isHit = false;
        }
    }


    //Controllers
    public void OnGuard() {
        if (isBlocking == true)
        {
            isBlocking = false;
        }
        else if (isBlocking == false)
        {
            isBlocking = true;
        }

        controller.SetBool("IsBlocking", isBlocking);
    }

    public void OnAttack1()
    {
        if (playerStamina >= 10f)
        {
            controller.SetTrigger("Attack 01");

            playerStamina = playerStamina - 10f;

            if (isHit == true)
            {
                lightAttack();
            }
        }
    }
    public void OnAttack2()
    {
        if (playerStamina >= 20f)
        {
            controller.SetTrigger("Attack 02");

            playerStamina = playerStamina - 20f;

            if (isHit == true)
            {
                strongAttack();
            }
        }
    }
    public void OnMove(InputValue inputValue)
    {
        moveInputValue = inputValue.Get<Vector2>();
    }

    public void MoveLogicMethod() {
        if (moveInputValue.x > 0)
        {
            isMoving = true;
            controller.SetBool("IsMoving", isMoving);

            horizontal = 1;
            rb.velocity = new Vector2(moveInputValue.x * speed, rb.velocity.y);
        }
        if (moveInputValue.x < 0)
        {
            isMoving = true;
            controller.SetBool("IsMoving", isMoving);

            horizontal = -1;
            rb.velocity = new Vector2(moveInputValue.x * speed, rb.velocity.y);
        }
        if (moveInputValue.x == 0)
        {
            isMoving = false;
            controller.SetBool("IsMoving", isMoving);
        }
    }
    private void FixedUpdate()
    {
        MoveLogicMethod();
    }


    public void OnJump() {
        if (IsGrounded() == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }

    public void OnMenuOpenClose() {
        MenuManager mm = GameObject.Find("MenuManager").GetComponent<MenuManager>();

        if (isPaused == false)
        {
            isPaused = true;

            enemy.GetComponent<Player2_Moves>().enabled = false;
            enemy.GetComponent<PlayerInput>().enabled = false;

            Time.timeScale = 0;
            mm.Pause(1);
        }
        else if (isPaused == true)
        {
            isPaused = false;

            enemy.GetComponent<Player2_Moves>().enabled = true;
            enemy.GetComponent<PlayerInput>().enabled = true;

            mm.Unpause(1);
            Time.timeScale = 1.0f;
        }
    }

    //Damage Phase
    public void lightAttack()
    {
        enemyController.SetTrigger("Light Hit");

        if (isBlockingEnemy == true)
        {
            playerHP = playerHP - (lightattackDmg / blockingOffset);
        }
        else
        {
            playerHP = playerHP - lightattackDmg;
        }
    }

    public void strongAttack()
    {
        enemyController.SetTrigger("Strong Hit");

        if (isBlockingEnemy == true)
        {
            playerHP = playerHP - (strongAttackDmg / blockingOffset);
        }
        else
        {
            playerHP = playerHP - strongAttackDmg;
        }
    }
        
    public void BindObjects() {
        GameObject placeholder;

        enemy = GameObject.Find("Player 2(Clone)");

        placeholder = GameObject.Find("Player2_hp");
        heathBar = placeholder.GetComponent<Image>();

        placeholder = GameObject.Find("Player1_Stamina");
        staminaBar = placeholder.GetComponent<Image>();

        placeholder = GameObject.Find("Player 1 - Light 1");
        gauge1 = placeholder.GetComponent<Image>();

        controller = character.GetComponent<Animator>();
        enemyController = enemy.GetComponent<Animator>();
    }

    public void FalseBind() {
        GameObject placeholder;

        enemy = GameObject.Find("DataFinder");

        placeholder = GameObject.Find("Player2_hp");
        heathBar = placeholder.GetComponent<Image>();

        placeholder = GameObject.Find("Player1_Stamina");
        staminaBar = placeholder.GetComponent<Image>();

        placeholder = GameObject.Find("Player 1 - Light 1");
        gauge1 = placeholder.GetComponent<Image>();

        controller = character.GetComponent<Animator>();
        enemyController = enemy.GetComponent<Animator>();
    }
}

