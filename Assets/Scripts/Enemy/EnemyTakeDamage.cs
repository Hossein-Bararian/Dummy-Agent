using System;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
   
   
    [SerializeField] private GameObject head;
    public bool isHeadCutted;
    private EnemyManager _enemyManager;
    private ToggleRagdoll _toggleRagdoll;
    private Animator _anim;

   
    private void Start()
    {
        _enemyManager = GetComponent<EnemyManager>();
        _toggleRagdoll = GetComponent<ToggleRagdoll>();
        _toggleRagdoll.Ragdoll(false);
        _anim = GetComponent<Animator>();
        isHeadCutted = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet") && !_enemyManager.isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        _anim.SetTrigger("Dead");
        _enemyManager.isDead = true;
       _enemyManager.DeActiveScripts();
        _toggleRagdoll.Ragdoll(true);
    }

    public void CutHead()
    {
        HingeJoint2D hinge = head.GetComponent<HingeJoint2D>();
        isHeadCutted = true;
        head.transform.SetParent(null);
        hinge.enabled = false;
        head.tag = "Enemy";
    }
}