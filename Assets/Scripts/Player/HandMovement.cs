using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandMovement : MonoBehaviour
{
    public Transform hand;
    [SerializeField] private Button[] uiButtons;
   
    private void LateUpdate()
    {
        MouseFollower();
    }
    private void MouseFollower()
    {
        if(!MenuManager.IsOnGame)return;
        if(StopMenuManager.IsGameStop) return;
        if (EventSystem.current.currentSelectedGameObject == uiButtons[0].gameObject) return;
        if (EventSystem.current.currentSelectedGameObject == uiButtons[1].gameObject) return;
        if (EventSystem.current.currentSelectedGameObject == uiButtons[2].gameObject) return;
       Vector3 mousePos = Input.mousePosition;
       if (mousePos.x >= 0 && mousePos.x <= Screen.width && mousePos.y >= 0 && mousePos.y <= Screen.height)
       {
           Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
           worldMousePos.z = 0;
           Vector3 lookDir = worldMousePos - hand.transform.position;
           float rotateZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
           hand.DORotateQuaternion(Quaternion.Euler(0, 0, rotateZ + 270f), .25f);
       }
    }
}