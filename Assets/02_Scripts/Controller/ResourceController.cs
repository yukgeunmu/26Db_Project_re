using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    private AnimationHandler animationHandler;
    private StatHandler statHandler;
    public float CurrentHealth { get; private set; }
    public float CurrentVelocity { get; private set; }
    public float CurrentJumpPower { get; private set; }
    public int CurrentJumpCount { get; private set; }
    public float CurrentJumpTime { get; private set; }
    public float CurrentJumpHeight { get; private set; }
    public float MaxHealth => statHandler.MaxHealth;

    public void OnAnimationIdle() => animationHandler.Idle();
    public void OnAnimationMove(Vector2 obj) => animationHandler.Moving(obj);
    public void OnAnimationJump(int jumpCount) => animationHandler.Jumping(jumpCount);
    public void OnAnimationSlide(bool isTrue) => animationHandler.Sliding(isTrue);

    private void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();
        CurrentHealth = statHandler.MaxHealth;
        CurrentVelocity = statHandler.MaxVelocity;
        CurrentJumpPower = statHandler.MaxJumpPower;
        CurrentJumpCount = statHandler.MaxJumpCount;
        CurrentJumpTime = statHandler.MaxJumpTime;
        CurrentJumpHeight = statHandler.MaxJumpHeight;
    }

    private void Start()
    {
        animationHandler.Init();
    }
     
    public void ChangeHealth(float damage)
    {
        CurrentHealth += damage;
    }
     
    public void ChangeSpeed(float speed)
    {
        CurrentVelocity = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            //collision.GetComponent<ItemInstance>().Take(ResourceController resourceController);
            Destroy(collision.gameObject);
        }
    }
}
