using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleManager particleManager;
    private EnemyTakeDamage _takeDamage;

    private void OnCollisionEnter2D(Collision2D other)
    {
        var bullet = gameObject;
        if (!(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player") ||
              other.gameObject.CompareTag("Head")))
        {
            particleManager.SpawnParticle(particleManager.bulletImpactParticles[0], bullet.transform);
        }
        else
        {
            particleManager.SpawnParticle(particleManager.bulletImpactParticles[1], bullet.transform);
        }

        if (other.gameObject.CompareTag("Head"))
        {
            _takeDamage = other.transform.root.GetComponent<EnemyTakeDamage>();
            if (!_takeDamage.isHeadCutted)
            {
                _takeDamage.CutHead();
            }
        }

        Destroy(bullet);
    }
}