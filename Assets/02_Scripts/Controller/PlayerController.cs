using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseCharacterController
{
    private Camera followCam;
    private float followCamY;

    float jumpTimeCounter = 0f;
    float maxJumpTime;

    Vector2 originalBoxColliderSize;
    Vector2 originalBoxColliderOffset;

    protected bool isJumping = false;
    protected bool isSlide = false;
    protected bool onGround = false;
    protected int JumpCount = 0; 

    protected void Start()
    {
        followCam = Camera.main;
        movementDirection = Vector2.right * resourceController.CurrentInitialVelocity;
        maxJumpTime = resourceController.CurrentJumpTime;
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        originalBoxColliderOffset = box.offset;
        originalBoxColliderSize = box.size;
        followCamY = transform.position.y + 1;
    }

    protected override void Update()
    {
        base.Update();
        followCam.transform.position = new Vector3(transform.position.x + 3, followCamY, followCam.transform.position.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = maxJumpTime;
            if(JumpCount < resourceController.CurrentJumpCount)
            {
                ++JumpCount;
                Jump();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

            Vector2 newSize = boxCollider.size;
            Vector2 newOffset = boxCollider.offset;

            float cutAmount = boxCollider.size.y / 2;
            newSize.y -= cutAmount;  // 세로 길이 감소
            newOffset.y -= cutAmount / 2.0f; // 아래쪽으로 이동하여 위쪽이 잘린 효과

            boxCollider.size = newSize;
            boxCollider.offset = newOffset;

            Slide(true);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

            boxCollider.size = originalBoxColliderSize;
            boxCollider.offset = originalBoxColliderOffset;

            Slide(false);
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (jumpTimeCounter <= 0) isJumping = false;
        if (isJumping && JumpCount < resourceController.CurrentJumpCount)
        {
            rb.velocity += new Vector2(0, resourceController.CurrentJumpPower);
        }
        if (!onGround && !isJumping)
        {
            rb.velocity += new Vector2(0, -1.0f);
        }
        jumpTimeCounter -= Time.fixedDeltaTime;
    }

    protected void Jump()
    {
        Vector2 jumpDirection = Vector2.up * resourceController.CurrentInitialJumpPower * JumpCount;

        rb.velocity = new Vector2(0, jumpDirection.y);
        resourceController.OnAnimationJump(JumpCount);

    }

    protected void Slide(bool isTrue)
    {
        resourceController.OnAnimationSlide(isTrue);
    }

    protected override void HandleAction()
    {
        resourceController.ChangeSpeed();
        movementDirection = Vector2.right;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            resourceController.OnAnimationJump(0);
            JumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
            resourceController.OnAnimationJump(JumpCount);
        }
    }
}
