using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseCharacterController
{
    [Header("Settings")]
    public bool CameraDoNotFollow = false;
    public bool DoNotMove = false;

    // 플레이어 따라다니는 카메라
    private Camera followCam;
    private float followCamY;

    // 슬라이딩에서 작아진 콜라이더를 원 상태로 복구시키기 위한 원본 데이터
    Vector2 originalBoxColliderSize;
    Vector2 originalBoxColliderOffset;

    // 점프 및 슬라이드를 하기 위한 조건
    protected bool onGround = false;
    protected bool isSlide = false;
    protected bool isJumping = false;
    protected int JumpCount = 0;
    float jumpTimeCounter = 0f;

    protected void Start()
    {
        // 카메라 변수 할당
        followCam = Camera.main;
        followCamY = transform.position.y + 1;

        // 콜라이더 원본 정보 저장
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
        // 활공 시간 끝나거나 점프 제한 높이 도달시 낙하. 땅에 닿으면 낙하 중단.
        if (jumpTimeCounter <= 0 || transform.position.y > resourceController.CurrentJumpHeight * JumpCount) 
            isJumping = false;
        if (!onGround && !isJumping)
            movementDirection.y = -1;
        else if (onGround && !isJumping)
            movementDirection.y = 0;

        // 버튼 눌림 감지
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

    // 땅에 닿으면 다시 점프 가능하도록 초기화
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            resourceController.OnAnimationJump(0);
            JumpCount = 0;
        }
    }

    // 땅에서 떨어지면 점프 애니메이션 재생
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
            resourceController.OnAnimationJump(JumpCount);
        }
    }
}
