using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Speed")] 
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;

    [Header("Ground Detect")] 
    [SerializeField]
    private Transform originRay;
    [SerializeField] private LayerMask layerMask;

    private Animator _anim;
    private Rigidbody2D _rb;
    

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Run();
        CheckInputs();
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
            _rb.velocity = new Vector2(jumpForce, jumpForce * 2.5f);
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
        RaycastHit2D groundRayCast = Physics2D.Raycast(originRay.position, Vector2.down, 1, layerMask);
        return groundRayCast;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(originRay.position, Vector2.down);
    }
}