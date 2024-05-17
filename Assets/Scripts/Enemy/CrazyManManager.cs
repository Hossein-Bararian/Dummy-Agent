using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CrazyManManager : MonoBehaviour
{
    [Header("BoxCast")] [SerializeField] private Vector3 castOffset;
    [SerializeField] private Vector3 castSize;
    [SerializeField] private float distance;
    [Space(35)] 
    [SerializeField] private float runSpeed;
    [SerializeField] private  AssetReferenceGameObject suicideParticle;
    private Animator _anim;
    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
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
            if (hit && hit.collider.CompareTag("Player"))
            {
                Attack(hit.collider.gameObject);
            }
        }
    }

    private void Attack(GameObject player)
    {
        _anim.Play("Walk");
        _rigidBody.velocity = new Vector2(runSpeed, _rigidBody.velocity.y);
        if (Vector3.Distance(transform.position, player.transform.Find("Hitman").transform.position) < distance)
        {
            Suicide(player);
        }
    }

    public void Suicide(GameObject player)
    {
       suicideParticle.InstantiateAsync(player.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
       if (player.gameObject.GetComponent<PlayerTakeDamage>())
           player.gameObject.GetComponent<PlayerTakeDamage>().Die();
       Destroy(gameObject,0.06f);
    }
    
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + castOffset, castSize);
    }
}