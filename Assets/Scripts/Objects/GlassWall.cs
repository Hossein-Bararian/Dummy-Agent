using UnityEngine;

public class GlassWall : MonoBehaviour
{
    [SerializeField] private ParticleSystem glassParticle;

    private void OnTriggerEnter2D(Collider2D other)
    {
            Instantiate(glassParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
    }
}