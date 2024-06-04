using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ParticleManager : MonoBehaviour
{
    public List<AssetReference> bulletImpactParticles;
    public void SpawnParticle(AssetReference particlePrefab, Transform particlePlace)
    {
        Addressables.InstantiateAsync(particlePrefab, particlePlace.position, particlePlace.rotation);
    }
}