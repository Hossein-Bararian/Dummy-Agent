using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    [Header("Dead Face")]
    [SerializeField] private Sprite deadEyeSprite;
    [SerializeField] private Sprite deadMouthSprite;
    [SerializeField] private GameObject[] eyes;
    [SerializeField] private GameObject mouth;
    [SerializeField]private GameOverManager gameOverManager;
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
            DeadFace();
            Die();
            gameOverManager.ActiveGameOverPanel(true);
            
        }
    }

    public void Die()
    {
        _toggleRagdoll.Ragdoll(true);
        PlayerManager.IsDead = true;
        _playerManager.DeActiveScripts();
    }
    private void DeadFace()
    {
        eyes[0].GetComponent<SpriteRenderer>().sprite = deadEyeSprite;
        eyes[1].GetComponent<SpriteRenderer>().sprite = deadEyeSprite;
        mouth.GetComponent<SpriteRenderer>().sprite = deadMouthSprite;
    }
}