using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FallDeath : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachin;
    [SerializeField]private GameOverManager gameOverManager;
    private PlayerTakeDamage _playerTakeDamage;
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerTakeDamage>())
            {
                _playerTakeDamage = other.GetComponent<PlayerTakeDamage>();
                cinemachin.Follow = null;
                yield return new WaitForSeconds(1);
                _playerTakeDamage.Die();
                gameOverManager.ActiveGameOverPanel(true);
            }

        }
    }
}
