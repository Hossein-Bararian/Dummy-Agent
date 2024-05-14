using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    void Start()
    {
        Destroy(gameObject,destroyTime);
    }
}
