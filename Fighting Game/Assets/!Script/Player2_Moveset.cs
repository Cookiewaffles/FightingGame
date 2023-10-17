using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player2_Moveset : MonoBehaviour
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

    public KeyCode k1;
    public KeyCode k2;
    public KeyCode k3;
    public KeyCode k4;
    public KeyCode k5;
    public KeyCode k6;


    //movement variable
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float horizontal;
    private float speed;
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

    // Start is called before the first frame update
    void Start()
    {
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


        //Action Keys
        if (Input.GetKeyDown(k1))
        {
            attack01();
        }

        if (Input.GetKeyDown(k2))
        {
            attack02();
        }


        if (Input.GetKeyDown(k3))
        {
            isBlocking = true;
            controller.SetBool("IsBlocking", isBlocking);
        }
        if (Input.GetKeyUp(k3))
        {
            isBlocking = false;
            controller.SetBool("IsBlocking", isBlocking);
        }


        //Movement Keys
        if (Input.GetKey(k4) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        if ((Input.GetKeyUp(k4)) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKey(k5))
        {
            isMoving = true;
            controller.SetBool("IsMoving", isMoving);

            horizontal = -1;
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        }
        if (Input.GetKeyUp(k5))
        {
            isMoving = false;
            controller.SetBool("IsMoving", isMoving);

            horizontal = 0;
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }

        if (Input.GetKey(k6))
        {
            isMoving = true;
            controller.SetBool("IsMoving", isMoving);

            horizontal = 1;
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        if (Input.GetKeyUp(k6))
        {
            isMoving = false;
            controller.SetBool("IsMoving", isMoving);

            horizontal = 0;
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }


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


    //attack animations
    public void attack01()
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

    public void attack02()
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

    public void SetBlocking()
    {
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
}
