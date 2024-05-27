using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ObjectDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name=="Body")
        {
            var parent = other.transform.parent.parent.gameObject;
           StartCoroutine(Destroyer(parent,2));
        }
        if (other.gameObject.CompareTag("Enemy") && other.name=="Head")
        {
          Destroy(other.gameObject,2);
        }
    }
    private IEnumerator Destroyer(GameObject obj,float time )
    {
        yield return new WaitForSeconds(time);
        Addressables.ReleaseInstance(obj);
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}