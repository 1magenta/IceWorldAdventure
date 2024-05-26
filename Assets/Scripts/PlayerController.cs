using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SIDE SCROLLER CONTROLLER
// Created for Understanding Game Engines
// Based on SharpCoder Blog's CharacterController2D
// https://sharpcoderblog.com/blog/2d-platformer-character-controller


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerController : MonoBehaviour
{
    // public variables
    public float maxSpeed = 1f;
    public float jumpHeight = 1f;
    public float gravityScale = 1f;

    // ground check variables
    public Vector2 groundOffset;
    public float groundRadius;
    public LayerMask layerMask;

    // private variables
    float moveDirection = 0;
    public bool isGrounded = false;
    private bool facingRight = true;

    Rigidbody2D rb;
    CapsuleCollider2D mainCollider;

    //------------------Set Audio to Player's action--------------------
    public AudioClip dieClip;
    public AudioClip footStepClip;
    public AudioClip jumpClip;
    public AudioClip shootClip;
    public AudioClip victoryClip;

    //------------------health setting-----------------------
    private int maxHealth = 5;
    private int currHealth;
    private float invincibleTime = 2f; //during this time Ruby won't loss health
    private float invincibleTimer;
    private bool isInvincible;
    public bool isAlive = true;



    public int MyMaxHealth { get { return maxHealth; } }
    public int MyCurrHealth { get { return currHealth; } }

    //-----------------snowBullet--------------------------------------------
    public GameObject snowBulletPrefab;
    private Vector2 snowBallDir;

    //----------------------animations---------------------------------
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        currHealth = 5;
        invincibleTimer = 0;

       // playerController = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.gravityScale = gravityScale;

        animator = GetComponentInChildren<Animator>();

        UIManager.instance.UpdateHealthBar(currHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // get our movement direction
        moveDirection = Input.GetAxis("Horizontal");

        // TODO: Facing L/R logic
        if (moveDirection < -0.01f && facingRight && isAlive)
        {
            // flip to left
            facingRight = false;
            Vector3 currentScale = transform.localScale;
            currentScale.x *= -1;
            transform.localScale = currentScale;

            AudioManager.instance.AudioPlay(footStepClip);

        } else if (moveDirection > 0.01f && !facingRight && isAlive)
        {
            // flip to right
            facingRight = true;
            Vector3 currentScale = transform.localScale;
            currentScale.x *= -1;
            transform.localScale = currentScale;

            AudioManager.instance.AudioPlay(footStepClip);
        }


        // Jump Controls
        if (Input.GetButtonDown("Jump") && isGrounded && isAlive)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            AudioManager.instance.AudioPlay(jumpClip);
        }

        //------------------------------Animation controls--------------------------
        if (animator && isAlive)
        {
            //set speed
            animator.SetFloat("Speed", Mathf.Abs(moveDirection));
            animator.SetBool("isGrounded", isGrounded);
        }
        //--------------------------------Invincible Timer--------------------------------
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }

        //-----------------------------Press"F", shoot bullet----------------------------
        if (Input.GetKeyDown(KeyCode.F) && isAlive)
        {
            if (animator)
            {
                animator.SetTrigger("Shoot");
            }
            AudioManager.instance.AudioPlay(shootClip);
            GameObject bullet = Instantiate(snowBulletPrefab, rb.position - Vector2.up * 0.5f, Quaternion.identity);
            BulletController bc = bullet.GetComponent<BulletController>();
            if (bc != null)
            {
                
                if (facingRight)
                {
                    snowBallDir = new Vector2(1, 0);
                }
                else
                {
                    snowBallDir = new Vector2(-1, 0);
                }
                bc.Move(snowBallDir, 1000);
            }
        }

    }

    private void FixedUpdate()
    {
        // Check for Ground Collisions
        isGrounded = false;

        // look for a collision
        Collider2D collider = Physics2D.OverlapCircle(getCollisionCenter(), groundRadius,layerMask);

        if (collider) { isGrounded = true; }


        // move the player
        if (isAlive)
        {
            rb.velocity = new Vector2((moveDirection) * maxSpeed, rb.velocity.y);
        }
        
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(getCollisionCenter(), groundRadius);
    }

    private Vector3 getCollisionCenter()
    {
        Vector3 collisionCenter = new Vector3();
        collisionCenter.x = transform.position.x + groundOffset.x;
        collisionCenter.y = transform.position.y + groundOffset.y;
        return collisionCenter;
    }

    /// <summary>
    /// set player status to death
    /// </summary>
    public void setDieStatus()
    {
        isAlive = false;
    }

    /// <summary>
    /// Change health
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible) return;
            isInvincible = true;
            if (animator)
            {
                animator.SetTrigger("Death");
            }
            AudioManager.instance.AudioPlay(dieClip);
            invincibleTimer = invincibleTime;
        }
        Debug.Log(currHealth + "/" + maxHealth);
        currHealth = Mathf.Clamp(currHealth + amount, 0, maxHealth);
        Debug.Log(currHealth + "/" + maxHealth);
        UIManager.instance.UpdateHealthBar(currHealth, maxHealth);

        if(currHealth <= 0)
        {
            isAlive = false;
            GameManager.S.EndGame();
            Destroy(this.gameObject, 1.0f);
            UIManager.instance.GameOverMessage();
            UIManager.instance.ShowButton();
        }

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        //--------------------Reach the Exit of the scene--------------------
        Debug.Log("penguin hit sth");
        if (collision.gameObject.CompareTag("ExitDoor"))
        {
            Debug.Log("door hit");
            GameManager.S.EndGame();
            AudioManager.instance.AudioPlay(victoryClip);
            UIManager.instance.YouWinMessage();
            UIManager.instance.ShowButton();
        }
    }

}
// 53-353 F23 W7