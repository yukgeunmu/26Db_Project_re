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
        // 조건에 따라 점프를 처리 (예: Input.GetButtonDown("Jump"))
        if (ShouldJump())
        {
            Jump(movementDirection);
        }
    }

    protected virtual void HandleAction()
    {
        // 추가 동작 처리
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
        // 점프 조건을 확인하는 로직을 구현합니다.
        // 예를 들어, 바닥에 붙어있을 때만 점프 가능하도록 구현할 수 있습니다.
        // 현재는 단순히 Input 체크 예시를 들었습니다.
        return IsJump && IsGrounded();
    }

    private bool IsGrounded()
    {
        // 캐릭터가 땅에 붙어있는지 확인하는 로직 (예: Raycast 사용)
        // 실제 게임 환경에 맞게 수정하세요.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f);
        return hit.collider != null;
    }

    private void Jump(Vector2 direction)
    {
        // 점프력에 따른 위쪽 방향 벡터 계산
        Vector2 jumpDirection = Vector2.up * resourceController.CurrentJumpPower;

        // Rigidbody에 점프 힘 추가 (즉시 속도 변경)
        rb.velocity = new Vector2(rb.velocity.x, jumpDirection.y);

        // 점프 애니메이션 호출
        resourceController.OnAnimationJump(jumpDirection);

        IsJump = false;

        // 만약 점프와 이동을 동시에 처리하고 싶다면 다음과 같이 결합할 수 있습니다.
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
