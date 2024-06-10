using System.Collections;
using DG.Tweening;
using UnityEngine;
public class EnemyShooting : MonoBehaviour
{
    [Header("BoxCast")]
    [SerializeField] private Vector3 castOffset;
    [SerializeField] private Vector3 castSize;
    
    [Space(30)] [Header("Dead Face Sprites")]
    [SerializeField] private Sprite mouthFindSprite;
    [SerializeField] private GameObject mouthSprite;
    
    [Space(30)] [Header("Shooting")]
    [SerializeField] private float bulletForce;
    [SerializeField] private float bulletGravity;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform hand;
    [SerializeField] private float shotDelay;
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
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position + castOffset, castSize, 0, Vector2.zero);

        foreach (var hit in hits)
        {
            if (hit && hit.collider.CompareTag("Player") && !_isShooting)
            {
                mouthSprite.GetComponent<SpriteRenderer>().sprite = mouthFindSprite;
                StartCoroutine(Shoot());
            }
        }
    }

    IEnumerator Shoot()
    {
        _anim.enabled = false;
        _isShooting = true;
        hand.transform.DORotate( new Vector3(0, 0, Random.Range(85, 95)), 0.2f);
        yield return new WaitForSeconds(0.23f);
        GameObject bulletInstance = EnemyBulletPoolManager.Instance.GetBullet();
        bulletInstance.transform.position = firePoint.position;
        bulletInstance.transform.rotation = hand.transform.rotation;
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
        rb.gravityScale = bulletGravity;
        rb.velocity = Vector2.zero; 
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