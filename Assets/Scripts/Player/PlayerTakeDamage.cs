using System;
using TMPro;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    
    private ToggleRagdoll _toggleRagdoll;
    private PlayerManager _playerManager;
    
    private void Awake()
    {
        _playerManager = GetComponent<PlayerManager>();
        _toggleRagdoll = GetComponent<ToggleRagdoll>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _toggleRagdoll.Ragdoll(true);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            _toggleRagdoll.Ragdoll(false);
        }
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
        _playerManager.DeActiveScripts();
        // dead face anim
    }
}