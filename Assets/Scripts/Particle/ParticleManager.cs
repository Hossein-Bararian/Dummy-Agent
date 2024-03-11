using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public List<ParticleSystem> bulletImpactParticles;
    public void SpawnParticle(ParticleSystem particlePrefab,Transform particlePlace)
    {
        var instance = Instantiate(particlePrefab, particlePlace.position, particlePlace.rotation);
     
    }
}
