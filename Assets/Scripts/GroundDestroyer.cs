using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GroundDestroyer : MonoBehaviour
{
    private bool _isGroundTouched=false;
    private bool _isOn=false;
   private void OnBecameInvisible()
   {
       if (!_isOn && _isGroundTouched)
           Destroy(gameObject);
   }
   
   private void OnCollisionEnter2D(Collision2D other)
   {
       if (other.gameObject.gameObject.CompareTag("Finish"))
       {
           _isGroundTouched = true;
           _isOn = true;
       }
   }
   private void OnCollisionExit2D(Collision2D other)
   {
       if (other.gameObject.gameObject.CompareTag("Finish"))
       {
           _isOn = false;
       }
   }

   private void OnDestroy()
   {
       Addressables.ReleaseInstance(gameObject);
   }
}
