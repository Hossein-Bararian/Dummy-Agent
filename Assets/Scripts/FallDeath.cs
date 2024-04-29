using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FallDeath : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachine;
    [SerializeField]private GameOverManager gameOverManager;
    private PlayerTakeDamage _playerTakeDamage;


    private void Awake()
    {
        cinemachine = FindObjectOfType<CinemachineVirtualCamera>();
        gameOverManager = FindObjectOfType<GameOverManager>(true);

    }

    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerTakeDamage>())
            {
                _playerTakeDamage = other.GetComponent<PlayerTakeDamage>();
                cinemachine.Follow = null;
                yield return new WaitForSeconds(1);
                _playerTakeDamage.Die();
                gameOverManager.ActiveGameOverPanel(true);
            }

        }
    }
}
