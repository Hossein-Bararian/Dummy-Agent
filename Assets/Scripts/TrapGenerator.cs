using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrapGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnList;
    private void Start()
    {
        int randomIndex = Random.Range(0, spawnList.Count);
        GameObject obj=Instantiate(spawnList[randomIndex], transform.position, Quaternion.identity);
        obj.transform.SetParent(transform);
    }
}
