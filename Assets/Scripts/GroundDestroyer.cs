using UnityEngine;
using UnityEngine.AddressableAssets;

public class GroundDestroyer : MonoBehaviour
{
    private bool _isGroundTouched=false;
    private bool _isOn=false;
   private void OnBecameInvisible()
   {
       if (gameObject.name == "FirstGround")
       {
           gameObject.SetActive(false);
       }
       if (!_isOn && _isGroundTouched)
       {
           Addressables.ReleaseInstance(gameObject);
       }
   }
   private void OnCollisionEnter2D(Collision2D other)
   {
       if (other.gameObject.CompareTag("Player"))
       {
           _isGroundTouched = true;
           _isOn = true;
       }
   }
   private void OnCollisionExit2D(Collision2D other)
   {
       if (other.gameObject.CompareTag("Player"))
       {
           _isOn = false;
       }
   }
}
