using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (script is EnemyTakeDamage)
                continue;
            Destroy(script);
        }
    }
}