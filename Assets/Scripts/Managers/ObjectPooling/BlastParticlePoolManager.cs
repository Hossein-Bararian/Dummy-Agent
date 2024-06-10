
using UnityEngine;
using UnityEngine.Pool;

public class BlastParticlePoolManager : MonoBehaviour
{
  
   public static BlastParticlePoolManager Instance;
   private ObjectPool<GameObject> _particlePool;
   [SerializeField] private GameObject blastParticlePrefabs;

   private void Awake()
   {
      Instance = this;
   }

   private void Start()
   {
      _particlePool = new ObjectPool<GameObject>(OnCreateParticle, OnGetParticle, OnReleaseParticle,
         OnDestroyParticle, true, 20, 40);
   }

   private void OnDestroyParticle(GameObject obj)
   {
      Destroy(obj);
   }

   private void OnReleaseParticle(GameObject obj)
   {
      obj.gameObject.SetActive(false);
   }

   private void OnGetParticle(GameObject obj)
   {
      obj.gameObject.SetActive(true);
   }

   private GameObject OnCreateParticle()
   {
      return Instantiate(blastParticlePrefabs);  
   }
   
   public GameObject GetParticle()
   {
      return _particlePool.Get();
   }

   public void ReleaseParticle(GameObject obj)
   { 
      if (obj.activeSelf)
      {
         _particlePool.Release(obj);
      }
   }
}
