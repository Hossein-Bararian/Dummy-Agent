using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandMovement : MonoBehaviour
{
    public Transform hand;
    [SerializeField] private Button btnSlowMotion;
    private void LateUpdate()
    {
        MouseFollower();
    }
    private void MouseFollower()
    {
       
        
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var lookDir = mousePos - hand.transform.position;
        float rotateZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        hand.DORotateQuaternion(Quaternion.Euler(0, 0, rotateZ + 270f), .25f);
     
        //JoyStick
        //
        // Vector2 joystickDirection = new Vector2(joystick.Horizontal, joystick.Vertical);
        // if (joystickDirection.magnitude > 0)
        // {
        //     float angle = Mathf.Atan2(joystickDirection.y, joystickDirection.x) * Mathf.Rad2Deg;
        //     hand.DORotateQuaternion(Quaternion.Euler(0, 0, angle + 250f), .35f);
        // }
        //JoyStick
        
    }
}