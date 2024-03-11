using System;
using System.Collections;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    public bool isDead;
    public bool isHeadCutted;
    [SerializeField] private ParticleSystem bloodParticle;
    [SerializeField] private GameObject head;
    private ToggleRagdoll _toggleRagdoll;
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _toggleRagdoll = GetComponent<ToggleRagdoll>();
        isHeadCutted = false;
        isDead = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet")||other.gameObject.CompareTag("Item"))
        {
            Die();
        }
    }

    private void Die()
    {
        _toggleRagdoll.Ragdoll(true);
        isDead = true;
        // dead face anim
        //cant shooting  
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