using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseCharacterController
{
    [Header("Settings")]
    public bool CameraDoNotFollow = false;
    public bool DoNotMove = false;

    // �÷��̾� ����ٴϴ� ī�޶�
    private Camera followCam;
    private float followCamY;

    // �����̵����� �۾��� �ݶ��̴��� �� ���·� ������Ű�� ���� ���� ������
    Vector2 originalBoxColliderSize;
    Vector2 originalBoxColliderOffset;

    // ���� �� �����̵带 �ϱ� ���� ����
    protected bool onGround = false;
    protected bool isSlide = false;
    protected bool isJumping = false;
    protected int JumpCount = 0;
    float jumpTimeCounter = 0f;

    protected void Start()
    {
        // ī�޶� ���� �Ҵ�
        followCam = Camera.main;
        followCamY = transform.position.y + 1;

        // �ݶ��̴� ���� ���� ����
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        originalBoxColliderOffset = box.offset;
        originalBoxColliderSize = box.size;
    }

    protected override void FixedUpdate()
    {
        if (!DoNotMove)
            base.FixedUpdate();
    }

    private void LateUpdate()
    {
        if (!CameraDoNotFollow)
            followCam.transform.position = new Vector3(transform.position.x + 3, followCamY, followCam.transform.position.z);
    }

    protected override void HandleAction()
    {
        // Ȱ�� �ð� �����ų� ���� ���� ���� ���޽� ����. ���� ������ ���� �ߴ�.
        if (jumpTimeCounter <= 0 || transform.position.y > resourceController.CurrentJumpHeight * JumpCount) 
            isJumping = false;
        if (!onGround && !isJumping)
            movementDirection.y = -1;
        else if (onGround && !isJumping)
            movementDirection.y = 0;

        // ��ư ���� ����
        switch (GetKeyDown())
        {
            case KeyCode.Space:
                if (JumpCount < resourceController.CurrentJumpCount && !isSlide)
                {
                    isJumping = true;
                    jumpTimeCounter = resourceController.CurrentJumpTime;
                    ++JumpCount;
                    movementDirection.y = 1;
                    resourceController.OnAnimationJump(JumpCount);
                }
                break;

            case KeyCode.LeftShift:
                if(!isJumping)
                {
                    isSlide = true;
                    BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
                    Vector2 newSize = boxCollider.size;
                    Vector2 newOffset = boxCollider.offset;

                    float cutAmount = boxCollider.size.y / 2;
                    newSize.y -= cutAmount;
                    newOffset.y -= cutAmount / 2.0f;

                    boxCollider.size = newSize;
                    boxCollider.offset = newOffset;
                    resourceController.OnAnimationSlide(isSlide);
                }
                break;
        }

        switch (GetKeyUp())
        {
            case KeyCode.Space:                
                if(isJumping)
                    isJumping = false;
                break;

            case KeyCode.LeftShift:
                if (isSlide)
                {
                    isSlide = false;
                    BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
                    boxCollider.size = originalBoxColliderSize;
                    boxCollider.offset = originalBoxColliderOffset;
                    resourceController.OnAnimationSlide(isSlide);
                }
                break;
        }
    }

    KeyCode? GetKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Space)) return KeyCode.Space;
        if (Input.GetKeyDown(KeyCode.LeftShift)) return KeyCode.LeftShift;
        return null;
    }

    KeyCode? GetKeyUp()
    {
        if (Input.GetKeyUp(KeyCode.Space)) return KeyCode.Space;
        if (Input.GetKeyUp(KeyCode.LeftShift)) return KeyCode.LeftShift;
        return null;
    }

    // ���� ������ �ٽ� ���� �����ϵ��� �ʱ�ȭ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            resourceController.OnAnimationJump(0);
            JumpCount = 0;
        }
    }

    // ������ �������� ���� �ִϸ��̼� ���
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
            resourceController.OnAnimationJump(JumpCount);
        }
    }
}
