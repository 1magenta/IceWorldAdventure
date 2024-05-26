using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float enemySpeed;

    private Rigidbody2D rb;
    public SpriteRenderer spriteRender;
    public bool facingLeft = true;

    public AudioClip defeatedClip;

    public bool isAlive = true;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        // spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (facingLeft && spriteRender.flipX == true)
        {
            spriteRender.flipX = false;
        } else if (!facingLeft && spriteRender.flipX == false)
        {
            spriteRender.flipX = true;

        }
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            Vector3 currentVelocity = rb.velocity;
            currentVelocity.x = enemySpeed;
            if (facingLeft) { currentVelocity.x *= -1f; }
            rb.velocity = currentVelocity;
        }
 

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TurnAround")
        {
            facingLeft = !facingLeft;
        }

        if (collision.gameObject.tag == "Player" && isAlive)
        {
            isAlive = false;
            AudioManager.instance.AudioPlay(defeatedClip);
            //Death animation
            if (animator)
            {
                animator.SetTrigger("Dying");
            }
            //Do not collide
            CircleCollider2D collider = GetComponent<CircleCollider2D>();
            collider.enabled = false;
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0f;
            UIManager.instance.UpdateScore(15);
            Destroy(this.gameObject, 1.5f);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isAlive)
            {
                Debug.Log("Player Killed");
                //Destroy(collision.gameObject);
                PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
                if (pc != null)
                {
                    //pc.DeathAnimation();
                    pc.ChangeHealth(-5);
                    //Destroy(pc.gameObject, 2.0f);
                    Debug.Log("Knock into an ememy");
                }
            }

        }   
    }


}
// 53-353 F23 W7