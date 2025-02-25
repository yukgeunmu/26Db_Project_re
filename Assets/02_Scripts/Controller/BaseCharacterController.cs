using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterController : MonoBehaviour
{
    protected ResourceController resourceController;
    protected Rigidbody2D rb;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    private bool IsJump = false;

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        resourceController = GetComponent<ResourceController>();
    }

    protected virtual void Update()
    {
        HandleAction();
        if (Input.GetKeyDown(KeyCode.Space)) IsJump = true;
        //Rotate(lookDirection);
    }

    protected virtual void FixedUpdate()
    {
        Movment(movementDirection);
        // ���ǿ� ���� ������ ó�� (��: Input.GetButtonDown("Jump"))
        if (ShouldJump())
        {
            Jump(movementDirection);
        }
    }

    protected virtual void HandleAction()
    {
        // �߰� ���� ó��
    }

    private void Movment(Vector2 direction)
    {
        direction = direction * resourceController.speed;
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }

        rb.velocity = new Vector2(direction.x, rb.velocity.y);

        resourceController.OnAnimationMove(direction);
    }

    private bool ShouldJump()
    {
        // ���� ������ Ȯ���ϴ� ������ �����մϴ�.
        // ���� ���, �ٴڿ� �پ����� ���� ���� �����ϵ��� ������ �� �ֽ��ϴ�.
        // ����� �ܼ��� Input üũ ���ø� ������ϴ�.
        return IsJump && IsGrounded();
    }

    private bool IsGrounded()
    {
        // ĳ���Ͱ� ���� �پ��ִ��� Ȯ���ϴ� ���� (��: Raycast ���)
        // ���� ���� ȯ�濡 �°� �����ϼ���.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f);
        return hit.collider != null;
    }

    private void Jump(Vector2 direction)
    {
        // �����¿� ���� ���� ���� ���� ���
        Vector2 jumpDirection = Vector2.up * resourceController.CurrentJumpPower;

        // Rigidbody�� ���� �� �߰� (��� �ӵ� ����)
        rb.velocity = new Vector2(rb.velocity.x, jumpDirection.y);

        // ���� �ִϸ��̼� ȣ��
        resourceController.OnAnimationJump(jumpDirection);

        IsJump = false;

        // ���� ������ �̵��� ���ÿ� ó���ϰ� �ʹٸ� ������ ���� ������ �� �ֽ��ϴ�.
        // direction += jumpDirection;
    }

    //private void Rotate(Vector2 direction)
    //{
    //    float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //    bool isLeft = Mathf.Abs(rotZ) > 90f;

    //    characterRenderer.flipX = isLeft;

    //    if (weaponPivot != null)
    //    {
    //        weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
    //    }
    //}

    //public void ApplyKnockback(Transform other, float power, float duration)
    //{
    //    knockbackDuration = duration;
    //    knockback = -(other.position - transform.position).normalized * power;
    //}
}
