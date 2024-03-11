using System;
using DG.Tweening;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    public Transform hand;
   

    private void Update()
    {
       
        MouseFollower();
    }

    private void MouseFollower()
    {
        if (Camera.main != null)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var lookDir = mousePos - hand.transform.position;
            float rotateZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            hand.DORotateQuaternion(Quaternion.Euler(0, 0, rotateZ + 90f), .35f);
        }
    }
}