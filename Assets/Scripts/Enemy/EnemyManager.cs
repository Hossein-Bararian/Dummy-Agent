using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static bool IsDead;

    private void Awake()
    {
        IsDead = false;
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