using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleManager particleManager;
    private EnemyTakeDamage _takeDamage;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!gameObject.activeSelf) return;
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
        if(gameObject.CompareTag("PlayerBullet"))
            PlayerBulletPoolManager.Instance.ReleaseBullet(gameObject);
        else  if(gameObject.CompareTag("EnemyBullet"))
            EnemyBulletPoolManager.Instance.ReleaseBullet(gameObject);
    }

    private void OnBecameInvisible()
    {
        if (!gameObject.activeSelf) return;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        Invoke("DeActiveBulletAfterTime", 0.15f);
    }

    private void DeActiveBulletAfterTime()
    {
        if (!gameObject.activeSelf) return;
        
        if(gameObject.CompareTag("PlayerBullet"))
            PlayerBulletPoolManager.Instance.ReleaseBullet(gameObject);
        else  if(gameObject.CompareTag("EnemyBullet"))
            EnemyBulletPoolManager.Instance.ReleaseBullet(gameObject);
    }

    private void OnEnable()
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }
}