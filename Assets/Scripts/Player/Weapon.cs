using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Weapon : MonoBehaviour
{
    [SerializeField]private Button btnSlowMotion;
    private enum WeaponsType
    {
        Pistol
    }
    [SerializeField] private Transform firePoint;
    [SerializeField] private WeaponsType weapon;
    [SerializeField] private  Shooting shooting;
    
    private void LateUpdate()
    {
        if (Input.GetButtonDown("Shot"))
        {
            if (weapon == WeaponsType.Pistol)
            {
                Pistol();
            }
        }
     
    }
    public void Pistol()
    {
        if(!MenuManager.IsOnGame)return;
        if (EventSystem.current.currentSelectedGameObject == btnSlowMotion.gameObject) return;
        shooting.Shot(80, 1, 0.25f, firePoint);
    }
}