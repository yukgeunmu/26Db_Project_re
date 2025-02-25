using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject obstaclePrefab; // ������ ��ֹ� ������
    public float spawnInterval = 2f; // ��ֹ� ���� ����
    public Vector2 spawnRangeX = new Vector2(-5f, 5f); // X�� ���� ����
    public Vector2 spawnRangeY = new Vector2(1f, 3f); // Y�� ���� ����

    void Start()
    {
        InvokeRepeating("SpawnObstacle", 1f, spawnInterval); // ���� �ð����� ��ֹ� ����
    }

    void SpawnObstacle()
    {
        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        float randomY = Random.Range(spawnRangeY.x, spawnRangeY.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
