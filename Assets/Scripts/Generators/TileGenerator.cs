using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TileGenerator : MonoBehaviour
{
    private const float PlayerDistanceLevelPart = 80f;

    [SerializeField] private Transform parentTile;
    [SerializeField] private Transform levelPartStart;
    [SerializeField] private List<AssetReferenceGameObject> levelPartList;
    [SerializeField] private GameObject player;
    private Vector3 _lastEndPosition;
    private bool isSpawning = false;

    private void Awake()
    {
        _lastEndPosition = levelPartStart.Find("EndPosition").position;
        int startingSpawnerLevelParts = 1;
        for (int i = 0; i < startingSpawnerLevelParts; i++)
        {
            SpawnLevelPart();
        }
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, _lastEndPosition) < PlayerDistanceLevelPart && !isSpawning)
        {
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        AssetReferenceGameObject chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
        isSpawning = true;
        Addressables.InstantiateAsync(chosenLevelPart, _lastEndPosition, Quaternion.identity).Completed += OnLevelPartSpawned;
    }

    private void OnLevelPartSpawned(AsyncOperationHandle<GameObject> handle)
    {
        Transform levelPartTransform = handle.Result.transform;
        levelPartTransform.SetParent(parentTile);
        _lastEndPosition = levelPartTransform.Find("EndPosition").position;
        isSpawning = false;
    }
}