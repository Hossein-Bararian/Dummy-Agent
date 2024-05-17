using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TileGenerator : MonoBehaviour
{
    private const float PlayerDistanceLevelPart=80f;

     [SerializeField] private Transform parentTile;
     [SerializeField] private Transform levelPartStart; 
     [SerializeField] private List<AssetReferenceGameObject> levelPartList; 
     [SerializeField] private GameObject player;
    
    private Vector3 _lastEndPosition;

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
            if (Vector3.Distance(player.transform.position, _lastEndPosition) < PlayerDistanceLevelPart)
            { 
                SpawnLevelPart();
            }
    }

    private void SpawnLevelPart()
    {
        Addressables.LoadAssetAsync<GameObject>(levelPartList[UnityEngine.Random.Range(0, levelPartList.Count)]).Completed += handle =>
        {
            Transform  chosenLevelPart = handle.Result.transform;
            Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart,_lastEndPosition);
            _lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        }; 
    }

    private Transform SpawnLevelPart(Transform levelPart,Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart,spawnPosition, Quaternion.identity);
        levelPartTransform.SetParent(parentTile);
        return levelPartTransform;
    }
}
