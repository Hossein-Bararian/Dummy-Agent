using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TrapGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnList;
    private void OnEnable()
    {
      GameObject trap =TrapPoolManager.Instance.GetTrap(spawnList[Random.Range(0,spawnList.Count)]);
      trap.transform.position = transform.position;
      trap.transform.rotation=Quaternion.identity;
      trap.transform.SetParent(transform);
    }
}