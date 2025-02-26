using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    private AnimationHandler animationHandler;
    private StatHandler statHandler;

    public float CurrentHealth { get; private set; }
    public float speed { get; private set; }
    public float CurrentVelocity { get; private set; }
    public float CurrentInitialVelocity { get; private set; }
    public float CurrentTerminalVelocity { get; private set; }
    public float CurrentInitialJumpPower { get; private set; }
    public float CurrentJumpPower { get; private set; }
    public int CurrentJumpCount { get; private set; }
    public float CurrentJumpTime { get; private set; }

    public void OnAnimationIdle() => animationHandler.Idle();
    public void OnAnimationMove(Vector2 obj) => animationHandler.Moving(obj);
    public void OnAnimationJump(int jumpCount) => animationHandler.Jumping(jumpCount);
    public void OnAnimationSlide(bool isTrue) => animationHandler.Sliding(isTrue);

    private void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();
        CurrentHealth = statHandler.MaxHealth;
        CurrentInitialVelocity = statHandler.MaxInitialVelocity;
        CurrentVelocity = statHandler.MaxVelocity;
        CurrentTerminalVelocity = statHandler.MaxTerminalVelocity;
        CurrentInitialJumpPower = statHandler.MaxInitialJumpPower;
        CurrentJumpPower = statHandler.MaxJumpPower;
        CurrentJumpCount = statHandler.MaxJumpCount;
        CurrentJumpTime = statHandler.MaxJumpTime;
    }

    private void Start()
    {
        animationHandler.Init();

        speed = CurrentInitialVelocity;
    }
     
    public void ChangeHealth(float damage)
    {
        CurrentHealth += damage;
    }
     
    public void ChangeSpeed()
    {
        speed = speed > CurrentTerminalVelocity ? CurrentTerminalVelocity : speed + CurrentVelocity;
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
