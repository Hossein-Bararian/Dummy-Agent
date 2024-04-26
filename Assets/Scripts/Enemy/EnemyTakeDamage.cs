using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyTakeDamage : MonoBehaviour
{
    private GameManager _gameManager;
    [Header("Dead Face")] [SerializeField] private Sprite deadEyeSprite;
    [SerializeField] private Sprite deadMouthSprite;
    [SerializeField] private GameObject[] eyes;
    [SerializeField] private GameObject mouth;
    [Space(10)] [SerializeField] private GameObject head;
    [Space(10)] public bool isHeadCutted;
    private EnemyManager _enemyManager;
    private ToggleRagdoll _toggleRagdoll;
    private Animator _playerAnim;
    private readonly string[] _playerHappyFacesAnimationName = { "HappyFace1", "HappyFace2" };

    private void Start()
    {
        isHeadCutted = false;
        _enemyManager = GetComponent<EnemyManager>();
        _toggleRagdoll = GetComponent<ToggleRagdoll>();
        _gameManager = FindObjectOfType<GameManager>();
        _playerAnim = GameObject.Find("Player").GetComponent<Animator>();
        _toggleRagdoll.Ragdoll(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet") && !_enemyManager.isDead)
        {
            if (_playerAnim != null)
            {
                int randomIndex = Random.Range(0, _playerHappyFacesAnimationName.Length);
                _playerAnim.Play(_playerHappyFacesAnimationName[randomIndex]);
            }
            DeadFace();
            Die();
        }
    }

    private void Die()
    {
        if(_gameManager!=null)  
            _gameManager.UpdateScore(1);
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
        if (_playerAnim != null)
        {
            _playerAnim.Play("PlayerCutHead");
        }
        if(_gameManager!=null)  
            _gameManager.UpdateScore(2);
        HingeJoint2D hinge = head.GetComponent<HingeJoint2D>();
        isHeadCutted = true;
        head.transform.SetParent(null);
        hinge.enabled = false;
        head.tag = "Enemy";
      
    }
}