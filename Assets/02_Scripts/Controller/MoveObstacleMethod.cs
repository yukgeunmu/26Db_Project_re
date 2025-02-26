using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class MoveObstacleMethod : MonoBehaviour
{
    [SerializeField][Range(0f, 10f)] private float speed = 1f;

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
