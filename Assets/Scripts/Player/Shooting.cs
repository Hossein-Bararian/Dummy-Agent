using UnityEngine;
using Cinemachine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float currentGunCoolDown;
    [SerializeField] private float impulseForce;
    private HandMovement _handMovement;
    private CinemachineImpulseSource _impulseSource;
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _handMovement = GetComponent<HandMovement>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        currentGunCoolDown -= Time.deltaTime;
    }

    public void Shot(float bulletSpeed, float bulletGravity, float gunCoolDown, Transform firePoint)
    {
        if (currentGunCoolDown > 0) return;
        if (_impulseSource == null) return;
        CameraShake.Instance.Shake(_impulseSource, impulseForce);
        currentGunCoolDown = gunCoolDown;

        GameObject bulletInstance = PlayerBulletPoolManager.Instance.GetBullet();
        bulletInstance.transform.position = firePoint.position;
        bulletInstance.transform.rotation = _handMovement.hand.transform.rotation;
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
        rb.gravityScale = bulletGravity;
        rb.velocity = Vector2.zero; 
        rb.AddForce(bulletSpeed * firePoint.right, ForceMode2D.Impulse);
        _anim.Play("GunRecoil");
    }
}