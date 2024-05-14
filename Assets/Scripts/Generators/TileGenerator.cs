using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_LEVEL_PART=80f;

    [SerializeField] private Transform parentTile;
    [SerializeField] private Transform levelPartStart; 
    [SerializeField] private List<Transform> levelPartList; 
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
        if (Vector3.Distance(player.transform.position, _lastEndPosition) < PLAYER_DISTANCE_LEVEL_PART)
        {
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        Transform chosenLevelPart = levelPartList[UnityEngine.Random.Range(0, levelPartList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart,_lastEndPosition);
        _lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;

    }

    private Transform SpawnLevelPart(Transform levelPart,Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart,spawnPosition, Quaternion.identity);
        levelPartTransform.SetParent(parentTile);
        return levelPartTransform;
    }
}
