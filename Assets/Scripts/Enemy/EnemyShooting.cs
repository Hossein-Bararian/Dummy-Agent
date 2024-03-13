using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;
using Random = UnityEngine.Random;


public class EnemyShooting : MonoBehaviour
{
    [Header("BoxCast")] [SerializeField] private Vector3 castOffset;
    [SerializeField] private Vector3 castSize;

    [Header("Shooting")] [SerializeField] private GameObject bulletPrefab;
    private bool _isShooting;
    [SerializeField] private float bulletForce;
    [SerializeField] private float bulletGravity;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform hand;
    [SerializeField] private float shotDelay;
    private Animator _anim;

    void Start()
    {
        _isShooting = false;
        _anim = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        CheckPlayerOnRange();
    }

    void CheckPlayerOnRange()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + castOffset, castSize, 0, Vector2.zero);
        if (hit.collider.gameObject != null)
        {
            if (hit.collider.CompareTag("Player") && !_isShooting)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    IEnumerator Shoot()
    {
        _isShooting = true;
        Vector3 randomRotation = new Vector3(0, 0, Random.Range(-85, -95));
        hand.transform.DORotate(randomRotation, 0.2f);
        yield return new WaitForSeconds(0.25f);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, UnityEngine.Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.gravityScale = bulletGravity;
        rb.AddForce(bulletForce * firePoint.right, ForceMode2D.Impulse);
        _anim.Play("GunRecoil");
        yield return new WaitForSeconds(shotDelay);
        _isShooting = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + castOffset, castSize);
    }
}