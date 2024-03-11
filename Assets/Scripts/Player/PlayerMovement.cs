using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [Header("Ground Detect")]
    [SerializeField] private Transform groundDetector;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerMask;
    private Animator _anim;
    private Rigidbody2D _rb;
    private  void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
    
    private void FixedUpdate()
    {
        Run();
    }

    private void Update()
    {
        CheckInputs();
    }

    private void Run()
    {
       _rb.velocity = new Vector2(runSpeed, _rb.velocity.y);
    }
    private void CheckInputs()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButtonDown("Slide"))
        {
            Slide();
        }
    }
    private void Jump()
    {
        if (IsGrounded())
        {
            _anim.Play("Jump");
            _rb.velocity = new Vector2(jumpForce, jumpForce*2.5f);
        }
    }
    private void Slide()
    {
        if (IsGrounded())
        {
            _anim.Play("Slide");
        }
    }
    
   private bool IsGrounded()
    {
        if (Physics2D.BoxCast(groundDetector.position, boxSize, 0, -groundDetector.up, radius, layerMask)) 
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundDetector.position - groundDetector.up*radius,boxSize);
    }
}