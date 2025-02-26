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
    [SerializeField][Range(0, 5)] private float maxInitialJumpPower = 1;
    [SerializeField][Range(0, 5)] private float maxJumpPower = 1;
    [SerializeField][Range(0, 5)] private float maxJumpTime = 1;
    [SerializeField][Range(0, 5)] private int maxJumpCount = 1;
    public float MaxHealth => maxHealth;
    public float MaxInitialVelocity => maxInitialVelocity;
    public float MaxVelocity => maxVelocity;
    public float MaxTerminalVelocity => maxTerminalVelocity;
    public float MaxInitialJumpPower => maxInitialJumpPower;
    public float MaxJumpPower => maxJumpPower;
    public float MaxJumpTime => maxJumpTime;
    public int MaxJumpCount => maxJumpCount;
}
