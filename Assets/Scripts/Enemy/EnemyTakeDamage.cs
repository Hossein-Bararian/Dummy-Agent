using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private GameManager _gameManager;
    [Header("Dead Face")] [SerializeField] private Sprite deadEyeSprite;
    [SerializeField] private Sprite deadMouthSprite;
    [SerializeField] private GameObject[] eyes;
    [SerializeField] private GameObject mouth;
    [Space(10)] 
    [SerializeField] private GameObject head;
    private ToggleRagdoll _toggleRagdoll;
    private EnemyManager _enemyManager;
    [Space(10)] public bool isHeadCut=false;
    private Animator _playerAnim;
    private readonly string[] _playerHappyFacesAnimationName = { "HappyFace1", "HappyFace2" };
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Vector3 headBloodPosition;
    private void Start()
    {
        _toggleRagdoll = GetComponent<ToggleRagdoll>();
        _enemyManager = GetComponent<EnemyManager>();
        _gameManager = FindObjectOfType<GameManager>();
        _playerAnim = GameObject.Find("Player").GetComponent<Animator>();
        _toggleRagdoll.Ragdoll(false);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet") && !_enemyManager.isDead)
        {
            if(_gameManager!=null)  
                _gameManager.UpdateScore(1);
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
        _enemyManager.isDead = true;
        _toggleRagdoll.Ragdoll(true);
        _enemyManager.ActiveScripts(false);
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
        HingeJoint2D hinge = head.GetComponent<HingeJoint2D>();
        isHeadCut = true;
        head.transform.SetParent(null);
        hinge.enabled = false;
        head.tag = "Enemy";
        if (_gameManager != null)
        {
            _gameManager.UpdateScore(1);
            _gameManager.UpdateHeadShots();
        }
        GameObject bodyBloodParticle = HeadBloodPoolManager.Instance.GetParticle();
        bodyBloodParticle.transform.SetParent(bodyTransform.transform,false);
        bodyBloodParticle.transform.localPosition =new Vector3(headBloodPosition.x,headBloodPosition.y,0);
        bodyBloodParticle.transform.localRotation = Quaternion.identity;
    }
}