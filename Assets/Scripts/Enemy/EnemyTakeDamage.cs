using System;
using System.Collections;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    [Header("Dead Face")] [SerializeField] private Sprite deadEyeSprite;
    [SerializeField] private Sprite deadMouthSprite;
    [SerializeField] private GameObject[] eyes;
    [SerializeField] private GameObject mouth;


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
            DeadFace();
            Die();
        }
    }

    private void Die()
    {
        _enemyManager.isDead = true;
        _enemyManager.DeActiveScripts();
        _toggleRagdoll.Ragdoll(true);
    }

    private void DeadFace()
    {
        eyes[0].GetComponent<SpriteRenderer>().sprite = deadEyeSprite;
        eyes[1].GetComponent<SpriteRenderer>().sprite = deadEyeSprite;
        mouth.GetComponent<SpriteRenderer>().sprite = deadMouthSprite;
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