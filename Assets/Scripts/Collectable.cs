using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public ParticleSystem CollectEffect;
    public AudioClip collectClip;

    /// <summary>
    /// Collision detect
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
        {
            if (pc.MyCurrHealth < pc.MyMaxHealth)
            {
                pc.ChangeHealth(1);
                Instantiate(CollectEffect, transform.position, Quaternion.identity);
                AudioManager.instance.AudioPlay(collectClip);
                Destroy(this.gameObject);
            }
            Debug.Log("collect!");
        }
    }
}
