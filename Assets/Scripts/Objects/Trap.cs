using UnityEngine;
using UnityEngine.AddressableAssets;

public class Trap : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerTakeDamage>())
            {
                other.gameObject.GetComponent<PlayerTakeDamage>().Die();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<EnemyManager>())
            {
                if (!other.gameObject.GetComponent<EnemyManager>().isDead &&
                    !other.gameObject.GetComponent<CrazyManManager>())
                    Addressables.ReleaseInstance(gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Addressables.ReleaseInstance(gameObject);
    }
}