using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDemo : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer thisSprite;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        thisSprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        thisSprite.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
    }


}
// 53-353 F23 W7