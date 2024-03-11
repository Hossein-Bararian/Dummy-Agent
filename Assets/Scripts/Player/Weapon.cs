using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponsType
    {
        Pistol,
        SMG,
        Shogun
    }
    
    [SerializeField] private Transform firePoint;
    [SerializeField] private WeaponsType weapon;
    [SerializeField]private  Shooting shooting;

    void Start()
    {
    }

    private void LateUpdate()
    {
        //bug button down and button for all gun smg an pistol must be split 
        if (Input.GetButtonDown("Shot"))
        {
            if (weapon == WeaponsType.Pistol)
            {
                Pistol();
            }
            else if (weapon == WeaponsType.Shogun)
            {
                Shotgun();
            }
            else if (weapon == WeaponsType.SMG)
            {
                SMG();
            }
        }
    }

    private void Pistol()
    {
        
            shooting.Shot(80, 1, 0.21f, firePoint);
        
    }

    private void Shotgun()
    {
            shooting.Shot(25, 3, 0.4f, firePoint);
    }

    private void SMG()
    {
            shooting.Shot(30, 2, 0.15f, firePoint);
    }
}