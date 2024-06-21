using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Weapon : MonoBehaviour
{
    public static Weapon Instance;
    [SerializeField] private Button[] uiButtons;
    private enum WeaponsType
    {
        Pistol
    }
    [SerializeField] private Transform firePoint;
    [SerializeField] private WeaponsType weapon;
    [SerializeField] private  Shooting shooting;

    private void Awake()
    {
        Instance = this;
    }

    private void LateUpdate()
    {
        if (Input.GetButtonUp("Shot"))
        {
            if (weapon == WeaponsType.Pistol)
            {
                Pistol();
            }
        }
     
    }
    public void Pistol()
    {
        if(PlayerMovement.IsOnJumpOrSlide) return;
        if(!MenuManager.IsOnGame)return;
        if(StopMenuManager.IsGameStop) return;
        if (EventSystem.current.currentSelectedGameObject == uiButtons[0].gameObject) return;
        if (EventSystem.current.currentSelectedGameObject == uiButtons[1].gameObject) return;
        if (EventSystem.current.currentSelectedGameObject == uiButtons[2].gameObject) return;
        shooting.Shot(80, 1, 0.24f, firePoint);
    }
}