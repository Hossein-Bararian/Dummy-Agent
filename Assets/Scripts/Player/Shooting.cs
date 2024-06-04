using UnityEngine;
using Cinemachine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Shooting : MonoBehaviour
{
    [SerializeField] private AssetReferenceGameObject bulletPrefab;
    [SerializeField] private float currentGunCoolDown;
    [SerializeField] private float impulseForce;
    private AsyncOperationHandle _handle;
    private HandMovement _handMovement;
    private CinemachineImpulseSource _impulseSource;
    private Animator _anim;

    private void Start()
    {
        _handle = Addressables.LoadAssetAsync<GameObject>(bulletPrefab);
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
        if (_handle.Status == AsyncOperationStatus.Succeeded)
        {
            Addressables.InstantiateAsync(bulletPrefab, firePoint.position, _handMovement.hand.transform.rotation)
                    .Completed +=
                bullet =>
                {
                    if (bullet.Status == AsyncOperationStatus.Succeeded)
                    {
                        GameObject bulletInstance = bullet.Result;
                        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
                        rb.gravityScale = bulletGravity;
                        rb.AddForce(bulletSpeed * firePoint.right, ForceMode2D.Impulse);
                        _anim.Play("GunRecoil");
                    }
                };
        }
    }
}