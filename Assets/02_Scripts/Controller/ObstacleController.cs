using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public GameObject obstaclePrefeb;
    public Vector2 spawnAreaMin = new Vector2(-5, 5); // X&Y�� �ּ� ��ġ
    public Vector2 spawnAreaMax = new Vector2(5, 5); // X&Y �ִ� ��ġ
    public float minSpawnInterval = 1.0f; // �ּ� ���� ����
    public float maxSpawnInterval = 1.0f; // �ִ� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            SpawnObstacles();
            float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval); // ���� ����
            yield return new WaitForSeconds(randomInterval);
        }
    }

    // Update is called once per frame
    void SpawnObject()
    {
        Vector2 randomPosition = new Vector2(Random.Range(spawnAreaMin.x, spawnAreaMax.x), Random.Range(spawnAreaMin.y, spawnAreaMax.y));
    }

    //Vector3 spawnPosition = new Vector3(randomPosition2D.x, randomPosition2D.y, 0);

    //Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
}
