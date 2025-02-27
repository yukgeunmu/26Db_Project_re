using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [Header("Health")]
    [SerializeField, Range(0f, 100f)] private float maxHealth = 1f;

    [Header("Velocity")]
    [SerializeField, Range(0f, 10f)] private float maxVelocity = 1f;

    [Header("Jump")]
    [SerializeField, Range(0, 10)] private float maxJumpPower = 1;
    [SerializeField, Range(0, 10)] private float maxJumpTime = 1;
    [SerializeField, Range(0, 10)] private float maxJumpHeight = 1;
    [SerializeField, Range(0, 5)] private int maxJumpCount = 1;
    public float MaxHealth => maxHealth;
    public float MaxVelocity => maxVelocity;
    public float MaxJumpPower => maxJumpPower;
    public float MaxJumpTime => maxJumpTime;
    public float MaxJumpHeight => maxJumpHeight;
    public int MaxJumpCount => maxJumpCount;
}
