using UnityEngine;
using Cinemachine;
using UnityEngine.AddressableAssets;

public class Shooting : MonoBehaviour
{
    [SerializeField] private AssetReferenceGameObject bulletPrefab;
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
        bulletPrefab.InstantiateAsync(firePoint.position, _handMovement.hand.transform.rotation).Completed +=
            handle =>
            {
                GameObject bullet = handle.Result;
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.gravityScale = bulletGravity;
                rb.AddForce(bulletSpeed * firePoint.right, ForceMode2D.Impulse);
                _anim.Play("GunRecoil");
            };
    }
}