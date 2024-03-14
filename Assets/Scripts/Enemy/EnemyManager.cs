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

    private void Update()
    {
    }

    public void DeActiveScripts()
    {
        var enemyScripts = GetComponents<Component>();
        foreach (var script in enemyScripts)
        {
            if (script is MonoBehaviour)
            {
                if (script is EnemyTakeDamage)
                    continue;
                Destroy(script);
            }
        }
    }
}