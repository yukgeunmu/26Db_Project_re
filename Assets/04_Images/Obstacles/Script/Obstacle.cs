using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject obstaclePrefab; // 생성할 장애물 프리팹
    public float spawnInterval = 2f; // 장애물 생성 간격
    public Vector2 spawnRangeX = new Vector2(-5f, 5f); // X축 랜덤 범위
    public Vector2 spawnRangeY = new Vector2(1f, 3f); // Y축 랜덤 범위

    void Start()
    {
        InvokeRepeating("SpawnObstacle", 1f, spawnInterval); // 일정 시간마다 장애물 생성
    }

    void SpawnObstacle()
    {
        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        float randomY = Random.Range(spawnRangeY.x, spawnRangeY.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
