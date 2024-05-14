using UnityEngine;

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
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<EnemyManager>())
            {
                if (!other.gameObject.GetComponent<EnemyManager>().isDead &&
                    !other.gameObject.GetComponent<CrazyManManager>())
                    Destroy(gameObject); 
            }
        }
    }
}