using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [Header("Touche Input")] 
    private Vector3 _startTouchePosition;
    private Vector3 _endTouchePosition;
    [Space(30)] 
    [Header("Movement Speed")] [SerializeField]
    public float runSpeed;
    [SerializeField] private float jumpForce;
    [Header("Ground Detect")]
    [SerializeField] private Transform originRay;
    [SerializeField] private LayerMask layerMask;
    [Space(30)] 
    [SerializeField] private ParallaxSystem parallaxSystem;
    private Animator _anim;
    private Rigidbody2D _rb;
    
    private void Start()
    {
        parallaxSystem.enabled = false;
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Run();
        CheckStopRunning();
        CheckInputs();
    }

    private void Update()
    {
        CheckInputs();
    }

    private void Run()
    {
        _rb.velocity = new Vector2(runSpeed, _rb.velocity.y);
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") && !parallaxSystem.enabled)
        {
            parallaxSystem.enabled = true;
        }
    }

    private void CheckStopRunning()
    {
        if (!this.transform.hasChanged)
        {
            parallaxSystem.enabled = false;
            _anim.Play("Idle");
        }
        transform.hasChanged = false;
    }

    private void CheckInputs()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
                _startTouchePosition = Input.GetTouch(0).position;
            
        }
        if(Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Ended && MenuManager.IsOnGame)
        {
            _endTouchePosition = Input.GetTouch(0).position;
            if (_startTouchePosition.y > _endTouchePosition.y+220)
            {
                Slide();
            }
            if (_startTouchePosition.y < _endTouchePosition.y-220)
            {
                Jump();
            }
        }
        
        // on Windows
        if (Input.GetButtonDown("Jump"))
        {
          
            Jump();
        }
        
        if (Input.GetButtonDown("Slide"))
        {
            Slide();
        }
        // on Windows
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Sliding"))
            {
                _anim.Play("Jump");
                _rb.velocity = new Vector2(jumpForce, jumpForce * 2.5f);
            }
        }
    }

    private void Slide()
    {
        if (IsGrounded())
        {
            _anim.Play("Sliding");
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D groundRayCast = Physics2D.Raycast(originRay.position, Vector2.down,1, layerMask);
        return groundRayCast;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(originRay.position, Vector2.down);
    }
}