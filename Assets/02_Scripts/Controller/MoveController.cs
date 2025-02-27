using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private float speed = 1f;

    private void Awake()
    {
        speed = GameManager.Instance.ObstacleSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        MoveElements(speed);
    }

    private void MoveElements(float speed)
    {
        this.transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
