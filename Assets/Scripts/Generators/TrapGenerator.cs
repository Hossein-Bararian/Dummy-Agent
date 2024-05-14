using System;
using System.Collections.Generic;
using UnityEngine;

public class TrapGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnList;
    private void Start()
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnList.Count);
        GameObject obj=Instantiate(spawnList[randomIndex], transform.position, Quaternion.identity);
        obj.transform.SetParent(transform);
    }
}
