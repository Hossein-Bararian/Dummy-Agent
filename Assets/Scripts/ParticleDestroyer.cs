using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    void Start()
    {
        Destroy(gameObject,destroyTime);
    }
}
