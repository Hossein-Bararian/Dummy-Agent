using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TrapPoolManager : MonoBehaviour
{ 
    private ObjectPool<GameObject> _trapPool;
    public static TrapPoolManager Instance; 
    private GameObject _trapPrefabs;
    
    private void Awake()
    {
        Instance = this;
        _trapPool =
            new ObjectPool<GameObject>(OnCreateTrap, OnGetTrap, OnReleaseTrap, OnDestroyTrap, true, 5, 10);
    }
    private void OnDestroyTrap(GameObject obj)
    {
        Destroy(obj);
    }

    private void OnReleaseTrap(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnGetTrap(GameObject obj)
    {
        obj.SetActive(true);
    }

    private GameObject OnCreateTrap()
    {
        return Instantiate(_trapPrefabs);
    }

    public GameObject GetTrap(GameObject obj)
    {
        _trapPrefabs = obj;
        return _trapPool.Get();
    }

    public void ReleaseTrap(GameObject trap)
    {
        _trapPool.Release(trap);
    }
}
