using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// To control the movement of bullet
/// </summary>
public class BulletController : MonoBehaviour
{
    Rigidbody2D rBody;
    public AudioClip hitObjClip;

    // Start is called before the first frame update
    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Bullet Movement
    /// </summary>
    /// <param name="moveDir"></param>
    /// <param name="moveForce"></param>
    public void Move(Vector2 moveDir, float moveForce)
    {
        rBody.AddForce(moveDir * moveForce);
    }


    //Snowball Collision Detection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("snowball");
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("snowball hit");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Player") == false)
        {
            Destroy(this.gameObject);
        }
            
    }
}
