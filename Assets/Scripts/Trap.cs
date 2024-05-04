using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerTakeDamage>())
            {
              other.gameObject.GetComponent<PlayerTakeDamage>().Die();
            }
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<EnemyManager>())
            {
                if (!other.gameObject.GetComponent<EnemyManager>().isDead)
                {
                    if (other.gameObject.GetComponent<CrazyManManager>())
                    {
                        other.gameObject.GetComponent<CrazyManManager>().Suicide(gameObject);
                    }
                    Destroy(gameObject);
                }
            }
        }
    }
}