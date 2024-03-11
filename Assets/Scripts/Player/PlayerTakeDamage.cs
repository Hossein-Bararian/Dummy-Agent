using System;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    public bool isDead;
    private ToggleRagdoll _toggleRagdoll;
    
    private void Awake()
    {
        _toggleRagdoll = GetComponent<ToggleRagdoll>();
        isDead = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Die();
        }
    }

    private void Die()
    {
        _toggleRagdoll.Ragdoll(true);
        isDead = true;
        // dead face anim
        //cant shooting  
    }
}