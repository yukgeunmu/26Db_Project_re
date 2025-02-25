using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [Header("Health")]
    [SerializeField][Range(0f, 100f)] private float maxHealth = 1f;

    [Header("Velocity")]
    [SerializeField][Range(0f, 10f)] private float maxInitialVelocity = 1f;
    [SerializeField][Range(0f, 10f)] private float maxVelocity = 1f;
    [SerializeField][Range(0f, 10f)] private float maxTerminalVelocity = 1f;

    [Header("Jump")]
    [SerializeField][Range(0f, 10f)] private float maxInitialJumpPower = 1f;
    [SerializeField][Range(0f, 10f)] private float maxJumpPower = 1f;
    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = Mathf.Clamp(value, 0f, 100f);
    }
    public float MaxInitialVelocity
    { 
        get => maxInitialVelocity;
        set => maxInitialVelocity = Mathf.Clamp(value, 0f, 100f);
    }
    public float MaxVelocity
    {
        get => maxVelocity;
        set => maxVelocity = Mathf.Clamp(value, 0f, 10f);
    }

    public float MaxTerminalVelocity
    {
        get => maxTerminalVelocity;
        set => maxTerminalVelocity = Mathf.Clamp(value, 0f, 10f);
    }

    public float MaxInitialJumpPower
    {
        get => maxInitialJumpPower;
        set => maxInitialJumpPower = Mathf.Clamp(value, 0f, 10f);
    }
    public float MaxJumpPower
    {
        get => maxJumpPower;
        set => maxJumpPower = Mathf.Clamp(value, 0f, 10f);
    }
}
