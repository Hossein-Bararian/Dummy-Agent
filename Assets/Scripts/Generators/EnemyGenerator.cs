using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private List<AssetReferenceGameObject> enemy;

    void OnEnable()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Addressables.ReleaseInstance(transform.GetChild(i).gameObject);
                print("Deleted"+transform.GetChild(i).gameObject.name);
            }
        }

        int randomIndex = Random.Range(0, enemy.Count);
        enemy[randomIndex].InstantiateAsync(transform.position, Quaternion.identity).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject obj = handle.Result;
                obj.transform.position = transform.position;
                obj.transform.SetParent(transform);
            }
        };
    }
}