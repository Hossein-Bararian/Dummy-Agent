using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool IsDead;

    private void Awake()
    {
        IsDead = false;
    }

    public void DeActiveScripts()
    {
        var playerScripts = GetComponents<MonoBehaviour>();
        var enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemyObjects)
        {
            var enemyScripts = enemy.GetComponents<MonoBehaviour>();
            foreach (var script in enemyScripts)
            {
                script.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                if (script.gameObject.GetComponent<Animator>())
                {
                    script.gameObject.GetComponent<Animator>().enabled = false;
                }
                Destroy(script);
            }
        }
        foreach (var script in playerScripts)
        {
            if (script is PlayerTakeDamage)
                continue;
            Destroy(script);
        }
    }
}