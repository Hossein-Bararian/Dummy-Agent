using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private EnemyTakeDamage _takeDamage; 
    private TrailRenderer _trailRenderer;
    private void Awake()
    {
     _trailRenderer=gameObject.transform.Find("Trial").GetComponent<TrailRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!gameObject.activeSelf) return;
        var bullet = gameObject;
        if (!(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player") ||
              other.gameObject.CompareTag("Head")))
        {

            GameObject particleInstance = BlastParticlePoolManager.Instance.GetParticle();
            particleInstance.transform.position = bullet.transform.position;
            particleInstance.transform.rotation = bullet.transform.rotation;
        }
        else
        {

            GameObject particleInstance = BloodParticlePoolManager.Instance.GetParticle();
            particleInstance.transform.position = bullet.transform.position;
            particleInstance.transform.rotation = bullet.transform.rotation;
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

        if (gameObject.CompareTag("PlayerBullet"))
            PlayerBulletPoolManager.Instance.ReleaseBullet(gameObject);
        else if (gameObject.CompareTag("EnemyBullet"))
            EnemyBulletPoolManager.Instance.ReleaseBullet(gameObject);
    }
    
    private void OnEnable()
    {
        if (_trailRenderer != null)
        {
            _trailRenderer.enabled = true;
        }
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }
    private void OnDisable()
    {
        if (_trailRenderer != null)
        {
            _trailRenderer.Clear();
            _trailRenderer.enabled = false;
        }
    }
}