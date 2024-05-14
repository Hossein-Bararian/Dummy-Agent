using System.Collections.Generic;
using UnityEngine;

public class ToggleRagdoll : MonoBehaviour
{
    
    public List<Rigidbody2D> rbs;
    public List<HingeJoint2D> joint;
    private BoxCollider2D _selfBoxCollider;
    private Rigidbody2D _selfRigidBody;
    private Animator _anim;


    private void Awake()
    {
        _selfBoxCollider = GetComponent<BoxCollider2D>();
        _selfRigidBody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        Ragdoll(false);
    }
    public void Ragdoll(bool isRagdoll)
    { 
        _anim.enabled = !isRagdoll;
        _selfBoxCollider.enabled = !isRagdoll;
        _selfRigidBody.simulated = !isRagdoll;
        
        foreach (var rb in rbs)
        {
            rb.simulated = isRagdoll;
        }
        foreach (var hinge in joint)
        {
            hinge.enabled = isRagdoll;
        }
    }
    

}
