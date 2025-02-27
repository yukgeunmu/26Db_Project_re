using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterController : MonoBehaviour
{
    protected ResourceController resourceController;
    protected Rigidbody2D rb;

    protected Vector2 movementDirection = Vector2.right;

    // 변수 할당
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        resourceController = GetComponent<ResourceController>();
    }

    // 움직임 값 계산
    protected virtual void Update()
    {
        HandleAction();
    }

    // 움직임 적용
    protected virtual void FixedUpdate()
    {
        Movment(movementDirection);
    }

    protected virtual void HandleAction()
    {
    }

    // 움직임 함수
    private void Movment(Vector2 direction)
    {
        rb.velocity = new Vector2(direction.x * resourceController.CurrentVelocity, direction.y * resourceController.CurrentJumpPower);
    }
}
