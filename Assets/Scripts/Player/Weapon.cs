using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
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
    private void Pistol()
    {
            shooting.Shot(80, 1, 0.21f, firePoint);
    }
}