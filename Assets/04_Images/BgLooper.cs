using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5; // 반복되는 배경 갯수

    public int obestacleCount = 0; // 장애물 갯수
    public Vector3 obstacleLastPosition = Vector3.zero; // 마지막으로 배치된 장애물의 위치

    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        obstacleLastPosition = obstacles[0].transform.position; // 첫번째 장애물의 위치 기준점
        obestacleCount = obstacles.Length; // 장애물 갯수 저장

        for (int i = 0; i < obestacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obestacleCount);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BackGround")) // 오브젝트가 트리거에 닿으면 끊기지 않도록 반복문
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;
            return;
        }

        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obestacleCount); // 장애물 재배치
        }
    }
}