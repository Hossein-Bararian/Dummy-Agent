using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class EnemyManager : MonoBehaviour
{
    public bool isDead;
    private bool _isOnGround;

    private void Awake()
    {
        _isOnGround=false;
        isDead = false;
    }
    public void DeActiveScripts()
    {
        var enemyScripts = GetComponents<MonoBehaviour>();
        foreach (var script in enemyScripts)
        {
            if (script is EnemyTakeDamage || script is EnemyManager)
                continue;
            (script).enabled = false;
        }
    }

    public void OnBecameInvisible()
    {
        if(!_isOnGround && !isDead)
            Invoke("DestroyObjectAfterTime", 0.5f);
            
        if (isDead)
            Invoke("DestroyObjectAfterTime", 1);
    }
    private void DestroyObjectAfterTime()
    {
        Addressables.ReleaseInstance(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isOnGround = true;
        }
   
    }
    private void OnDestroy()
    {
        Addressables.ReleaseInstance(gameObject);
    }
    
}