using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TrapGenerator : MonoBehaviour
{
    [SerializeField] private List<AssetReferenceGameObject> spawnList;

    private void Start()
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnList.Count);
        Addressables.InstantiateAsync(spawnList[randomIndex], transform.position, Quaternion.identity).Completed +=
            handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject obj = handle.Result;
                    obj.transform.SetParent(transform);
                }
            };
    }
}