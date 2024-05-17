using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private List<AssetReferenceGameObject> enemy;

    [SerializeField] private bool isRight;

    void Start()
    {
        int randomIndex = Random.Range(0, enemy.Count);
        enemy[randomIndex].InstantiateAsync(transform.position, Quaternion.identity).Completed += handle =>
        {
            GameObject obj = handle.Result;
            obj.transform.SetParent(transform);
            if (obj.GetComponent<EnemyShooting>())
            {
                if (isRight)
                {
                    obj.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                }
                obj.GetComponent<EnemyShooting>().isRightSide = isRight;
            }
        };
    }
}