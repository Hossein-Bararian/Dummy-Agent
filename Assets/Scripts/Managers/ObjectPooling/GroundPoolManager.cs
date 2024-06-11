using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GroundPoolManager : MonoBehaviour
{
    public static GroundPoolManager Instance;
    [SerializeField] private List<GameObject> groundsPrefab;
    private ObjectPool<GameObject> _groundPool;
    private int _index = 0;

    private void Awake()
    {
        Shuffle(groundsPrefab);
        Instance = this;
        _groundPool = new ObjectPool<GameObject>(
            OnCreateGround, OnGetGround, OnReleaseGround, OnDestroyGround,
            true, 10, 20);
    }

    private void OnDestroyGround(GameObject obj)
    {
        Destroy(obj.gameObject);
    }

    private void OnReleaseGround(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnGetGround(GameObject obj)
    {
        obj.gameObject.SetActive(true);
    }

    private GameObject OnCreateGround()
    {
        if (_index > groundsPrefab.Count - 1)
        {
            _index = 0;
            Shuffle(groundsPrefab);
        }
        return Instantiate(groundsPrefab[_index++]);
    }

    public GameObject GetGround()
    {
        return _groundPool.Get();
    }

    public void ReleaseGround(GameObject obj)
    {
        _groundPool.Release(obj);
    }

    private void Shuffle(List<GameObject> obj)
    {
        for (int t = 0; t < obj.Count; t++)
        {
            GameObject tmp = obj[t];
            int r = Random.Range(t, obj.Count);
            obj[t] = obj[r];
            obj[r] = tmp;
        }
    }
}