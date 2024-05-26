using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public AudioClip collectCoinClip;


    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
        {
            Debug.Log("coin!");
            AudioManager.instance.AudioPlay(collectCoinClip);
            Destroy(this.gameObject);
            UIManager.instance.UpdateScore(1);
            
        }
    }
}
