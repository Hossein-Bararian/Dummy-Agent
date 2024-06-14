using System.Collections.Generic;
using UnityEngine;

public class TrapGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnList;
    private void OnEnable()
    {
        int randomIndex = Random.Range(0, 2);
        if (randomIndex == 0)
        {
            GameObject trap = TrapPoolManager.Instance.GetTrap(spawnList[Random.Range(0, spawnList.Count)]);
            trap.transform.position = transform.position;
            trap.transform.rotation = Quaternion.identity;
            trap.transform.SetParent(transform);
        }
    }
}