using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
            if (other.gameObject.name == "BulletDestroyer")
                Destroy(bullet);
            else
                particleManager.SpawnParticle(particleManager.bulletImpactParticles[0], bullet.transform);
        }
        else
        {
            particleManager.SpawnParticle(particleManager.bulletImpactParticles[1], bullet.transform);
        }

        if (other.gameObject.CompareTag("Head"))
        {
                _takeDamage = other.transform.parent.parent.GetComponent<EnemyTakeDamage>();
                try
                {
                    if (!_takeDamage.isHeadCutted && _takeDamage)
                        _takeDamage.CutHead();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                } 
        }
        Destroy(bullet);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}