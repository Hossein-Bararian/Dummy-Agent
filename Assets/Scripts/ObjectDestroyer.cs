using System;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
      Destroy(other.gameObject,5);
    }
}