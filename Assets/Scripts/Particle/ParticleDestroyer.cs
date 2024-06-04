using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ParticleDestroyer : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    void Start()
    {
       StartCoroutine(DestroyParticle());
    }
    IEnumerator DestroyParticle()
    {
        yield return new WaitForSeconds(destroyTime);
        Addressables.ReleaseInstance(gameObject);
    }
}
