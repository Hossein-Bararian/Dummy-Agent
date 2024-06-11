using System;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    private const float PlayerDistanceLevelPart = 80f;

    [SerializeField] private Transform parentTile;
    [SerializeField] private Transform levelPartStart;
    [SerializeField] private GameObject player;
    private Vector3 _lastEndPosition;
    private bool _isSpawning = false;

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
        if (Vector3.Distance(player.transform.position, _lastEndPosition) < PlayerDistanceLevelPart && !_isSpawning)
        {
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        _isSpawning = true;
        GameObject chosenLevelPart = GroundPoolManager.Instance.GetGround();
        chosenLevelPart.transform.position = _lastEndPosition;
        chosenLevelPart.transform.rotation = Quaternion.identity;
        chosenLevelPart.transform.SetParent(parentTile);
        _lastEndPosition = chosenLevelPart.transform.Find("EndPosition").position;
        _isSpawning = false;
    }
}