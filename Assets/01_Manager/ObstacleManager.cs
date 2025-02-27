using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private PlayerController player;

    [Header("��ֹ� ������Ʈ")]
    public GameObject[] Obstacle; // ���������� ��� ������ �迭 (�θ� ���)

    public float bottom_position = 0f;
    public float spawnStartX = 5 ; // X�� ���� ��ġ
    private float lastSpawnX; // ������ ��ֹ��� X ��ġ
    private float spawnGapX;

    private float spawnInterval; // �⺻ ���� ����
    public float minSpawnInterval = 0.3f; // �ּ� ���� ���� (�ʹ� ������ �ʵ��� ����)
    public float speedFactor = 0.05f; // �÷��̾� �ӵ��� ���� ������

    public Vector2 minScale = new Vector2(5f, 5f); // �ּ� ũ��
    public Vector2 maxScale = new Vector2(10f, 30f); // �ִ� ũ��


    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        lastSpawnX = spawnStartX; // X ��ġ �ʱ�ȭ
        spawnGapX = Random.Range(5, 10); // ���� ���� ����
        StartCoroutine(SpawnObstacleRoutine());
    }

    IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
            SpawnObstacle();
            spawnInterval = GameManager.Instance.ObstacleTime;
            speedFactor = GameManager.Instance.ObstacleFactor;
            float adjustedInterval = Mathf.Max(minSpawnInterval, spawnInterval - speedFactor); // �ּҰ� ����
            yield return new WaitForSeconds(adjustedInterval); // �������� ���� �ֱ� ����
        }
    }

    void SpawnObstacle()
    {
        int index = Random.Range(0, Obstacle.Length); // �迭���� �������� ��ֹ� ����

        // X�� ��ġ�� ���� �������� ����
        if (index <= 2) bottom_position = -3f;
        else if (index > 2) bottom_position = Random.Range(-2f, -1.5f);

        Vector3 spawnPosition = new Vector3(lastSpawnX, bottom_position, 10);
        GameObject newObstacle = Instantiate(Obstacle[index], spawnPosition, Quaternion.identity);

        float randomScaleX = Random.Range(minScale.x, maxScale.x);
        float randomScaleY = Random.Range(minScale.y, maxScale.y);
        newObstacle.transform.localScale = new Vector3(randomScaleX, randomScaleY, 1f);

        //���� ���� ��ġ ������Ʈ(���� ���� ����)
        spawnGapX = Random.Range(5, 10); // ���ο� ���� ���� ����
        lastSpawnX += spawnGapX;

        if (lastSpawnX > 100f)
        {
            lastSpawnX = spawnStartX;
        }

    }


}
