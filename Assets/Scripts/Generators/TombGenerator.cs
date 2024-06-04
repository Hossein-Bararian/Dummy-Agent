using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TombGenerator : MonoBehaviour
{
    [SerializeField] private AssetReferenceGameObject spawn;
    [SerializeField] private GameObject trapGenerator;

    private void Awake()
    {
        int randomIndex = Random.Range(0, 3);
        if (randomIndex == 1)
        {
            if (trapGenerator != null)
                trapGenerator.SetActive(false);
            Addressables.InstantiateAsync(spawn, transform.position, Quaternion.identity).Completed += handle =>
                {
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        GameObject obj = handle.Result;
                        obj.transform.SetParent(transform);
                    }
                };
        }
    }
}