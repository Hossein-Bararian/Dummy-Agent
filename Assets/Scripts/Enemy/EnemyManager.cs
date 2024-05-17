using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class EnemyManager : MonoBehaviour
{
    public bool isDead;

    private void Awake()
    {
        isDead = false;
    }
    public void DeActiveScripts()
    {
        var enemyScripts = GetComponents<MonoBehaviour>();
        foreach (var script in enemyScripts)
        {
            if (script is EnemyTakeDamage || script is EnemyManager)
                continue;
            Destroy(script);
        }
    }
    void OnDestroy()
    {
        Addressables.ReleaseInstance(gameObject);
    }
}