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
               Destroy(script);
           }
       }
        foreach (var script in playerScripts)
        {
           Destroy(script);
        }
    }
}
