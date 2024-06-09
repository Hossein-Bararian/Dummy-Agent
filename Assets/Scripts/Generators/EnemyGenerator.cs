using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private List<AssetReferenceGameObject> enemy;
    void Awake()   
    {
        int randomIndex = Random.Range(0, enemy.Count);
        enemy[randomIndex].InstantiateAsync(transform.position, Quaternion.identity).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject obj = handle.Result;
                obj.transform.SetParent(transform);
            }
        };
    }
}