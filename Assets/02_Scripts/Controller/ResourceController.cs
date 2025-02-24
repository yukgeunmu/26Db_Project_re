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
    public float MaxHealth => statHandler.MaxHealth;
    public float MaxInitialVelocity => statHandler.MaxInitialVelocity;
    public float MaxVelocity => statHandler.MaxVelocity;
    public float MaxTerminalVelocity => statHandler.MaxTerminalVelocity;
    public float MaxInitialJumpPower => statHandler.MaxInitialJumpPower;
    public float MaxJumpPower => statHandler.MaxJumpPower;
    public void OnAnimationIdle() => animationHandler.Idle();
    public void OnAnimationMove(Vector2 obj) => animationHandler.Moving(obj);
    public void OnAnimationJump(Vector2 obj) => animationHandler.Jumping(obj);

    private void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();
    }

    private void Start()
    {
        animationHandler.Init();
        CurrentHealth = MaxHealth;
        CurrentInitialVelocity = MaxInitialVelocity;
        CurrentVelocity = MaxVelocity;
        CurrentTerminalVelocity = MaxTerminalVelocity;
        CurrentInitialJumpPower = MaxInitialJumpPower;
        CurrentJumpPower = MaxJumpPower;

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
