using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsIdle = Animator.StringToHash("IsIdle");
    //private static readonly int IsDead = Animator.StringToHash("IsDead");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    //private static readonly int IsDamaged = Animator.StringToHash("IsDamaged");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    //private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");

    protected Animator animator;

    public void Init()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Idle()
    {
        animator.SetBool(IsIdle, true);
    }

    public void Moving(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);
    }
    public void Jumping(Vector2 obj)
    {
        animator.SetTrigger(IsJumping);
    }

    //public void Attacking()
    //{
    //    animator.SetBool(IsAttacking, true);
    //}

    //public void Damaged()
    //{
    //    animator.SetBool(IsDamaged, true);
    //}
    //public void Die()
    //{
    //    animator.SetBool(IsDead, true);
    //}
}
