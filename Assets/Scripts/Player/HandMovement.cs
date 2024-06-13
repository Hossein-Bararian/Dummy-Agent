using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandMovement : MonoBehaviour
{
    public Transform hand;
    [SerializeField] private Button btnSlowMotion;
    [SerializeField] private Button btnShot;
    [SerializeField] private Button btnTapToStart;
    private void LateUpdate()
    {
        MouseFollower();
    }
    private void MouseFollower()
    {
        if(!MenuManager.IsOnGame)return;
        if (EventSystem.current.currentSelectedGameObject == btnSlowMotion.gameObject) return;
        if (EventSystem.current.currentSelectedGameObject == btnShot.gameObject) return;
        if (EventSystem.current.currentSelectedGameObject == btnTapToStart.gameObject) return;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var lookDir = mousePos - hand.transform.position;
        float rotateZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        hand.DORotateQuaternion(Quaternion.Euler(0, 0, rotateZ + 270f), .25f);
    }
}