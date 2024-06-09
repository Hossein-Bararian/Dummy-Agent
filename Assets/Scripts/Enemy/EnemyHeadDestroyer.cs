using UnityEngine;
using UnityEngine.AddressableAssets;

public class EnemyHeadDestroyer : MonoBehaviour
{
    private void OnBecameInvisible()
    {
      gameObject.SetActive(false);
    }
}
