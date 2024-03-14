using System;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    
    private ToggleRagdoll _toggleRagdoll;
    
    private void Awake()
    {
        _toggleRagdoll = GetComponent<ToggleRagdoll>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet") && !PlayerManager.IsDead)
        {
            Die();
        }
    }

    private void Die()
    {
        _toggleRagdoll.Ragdoll(true);
        PlayerManager.IsDead = true;
        // dead face anim
        //cant shooting  
    }
}