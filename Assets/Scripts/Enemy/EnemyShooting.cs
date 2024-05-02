using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class EnemyShooting : MonoBehaviour
{
    [Header("BoxCast")] 
    [SerializeField] private Vector3 castOffset;
    [SerializeField] private Vector3 castSize;
      [Space(30)]
    [Header("Dead Face Sprites")]
    [SerializeField] private Sprite mouthFindSprite;
    [SerializeField] private GameObject mouthSprite;
    [Space(30)]
    [Header("Shooting")] 
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce;
    [SerializeField] private float bulletGravity;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform hand;
    [SerializeField] private float shotDelay;
     public bool isRightSide;
    
    private Animator _anim;
    private bool _isShooting;

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
        try
        {
            if (hit.collider.gameObject != null)
            {
                if (hit.collider.CompareTag("Player") && !_isShooting)
                {
                    mouthSprite.GetComponent<SpriteRenderer>().sprite = mouthFindSprite;
                    StartCoroutine(Shoot());
                }
            }
        }
        catch (Exception e)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Shoot()
    {
        _anim.enabled = false;
        _isShooting = true;
        hand.transform.DORotate(CheckEnemySide(), 0.2f);
        yield return new WaitForSeconds(0.25f);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, hand.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.gravityScale = bulletGravity;
        rb.AddForce(bulletForce * firePoint.right, ForceMode2D.Impulse);
        _anim.Play("GunRecoil");
        yield return new WaitForSeconds(shotDelay);
        _isShooting = false;
    }

    private Vector3 CheckEnemySide()
    {
        if (isRightSide)
        {
            firePoint.transform.rotation =new Quaternion(0,0,0,0);
           return new Vector3(0, 0, Random.Range(-85, -95));
        }
        else
        {
            return new Vector3(0, 0, Random.Range(85, 95));
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + castOffset, castSize);
    }
}