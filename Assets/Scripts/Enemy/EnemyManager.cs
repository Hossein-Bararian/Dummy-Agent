using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public bool isDead;
    [Header("BoxCast")] [SerializeField] private Vector3 castOffset;
    [SerializeField] private float radius;

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