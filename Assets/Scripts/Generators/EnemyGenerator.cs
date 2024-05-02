using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemy;
    [SerializeField] private bool isRight;
    void Start()
    {
        int randomIndex = Random.Range(0, enemy.Count);
        GameObject obj=Instantiate(enemy[randomIndex], transform.position, Quaternion.identity);
        if (obj.GetComponent<EnemyShooting>())
        {
            if (isRight)
            {
                obj.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            obj.GetComponent<EnemyShooting>().isRightSide = isRight;
            
        }
    }

   
}
