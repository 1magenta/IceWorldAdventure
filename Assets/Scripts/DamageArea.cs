using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Damage area, Ruby will loss health
/// </summary>
public class DamageArea : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.ChangeHealth(-1);
        }
    }
}
