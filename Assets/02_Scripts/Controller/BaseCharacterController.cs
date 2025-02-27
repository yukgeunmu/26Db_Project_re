using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterController : MonoBehaviour
{
    protected ResourceController resourceController;
    protected Rigidbody2D rb;

    protected Vector2 movementDirection = Vector2.right;

    // ���� �Ҵ�
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        resourceController = GetComponent<ResourceController>();
    }

    // ������ �� ���
    protected virtual void Update()
    {
        HandleAction();
    }

    // ������ ����
    protected virtual void FixedUpdate()
    {
        Movment(movementDirection);
    }

    protected virtual void HandleAction()
    {
    }

    // ������ �Լ�
    private void Movment(Vector2 direction)
    {
        rb.velocity = new Vector2(direction.x * resourceController.CurrentVelocity, direction.y * resourceController.CurrentJumpPower);
    }
}
