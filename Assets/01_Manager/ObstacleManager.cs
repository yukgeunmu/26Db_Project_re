using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private PlayerController player;

    [Header("장애물 오브젝트")]
    public GameObject[] Obstacle; // 스테이지별 배경 프리팹 배열 (부모 배경)

    public float bottom_position = 0f;
    public float spawnStartX = 5 ; // X축 시작 위치
    private float lastSpawnX; // 마지막 장애물의 X 위치
    private float spawnGapX;

    private float spawnInterval; // 기본 생성 간격
    public float minSpawnInterval = 0.3f; // 최소 생성 간격 (너무 빠르지 않도록 제한)
    public float speedFactor = 0.05f; // 플레이어 속도에 따라 감소할

    public Vector2 minScale = new Vector2(5f, 5f); // 최소 크기
    public Vector2 maxScale = new Vector2(10f, 30f); // 최대 크기


    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        lastSpawnX = spawnStartX; // X 위치 초기화
        spawnGapX = Random.Range(5, 10); // 랜덤 간격 설정
        StartCoroutine(SpawnObstacleRoutine());
    }

    IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
            SpawnObstacle();
            spawnInterval = GameManager.Instance.ObstacleTime;
            speedFactor = GameManager.Instance.ObstacleFactor;
            float adjustedInterval = Mathf.Max(minSpawnInterval, spawnInterval - speedFactor); // 최소값 제한
            yield return new WaitForSeconds(adjustedInterval); // 동적으로 생성 주기 변경
        }
    }

    void SpawnObstacle()
    {
        int index = Random.Range(0, Obstacle.Length); // 배열에서 랜덤으로 장애물 선택

        // X축 위치를 일정 간격으로 조정
        if (index <= 2) bottom_position = -3f;
        else if (index > 2) bottom_position = Random.Range(-2f, -1.5f);

        Vector3 spawnPosition = new Vector3(lastSpawnX, bottom_position, 10);
        GameObject newObstacle = Instantiate(Obstacle[index], spawnPosition, Quaternion.identity);

        float randomScaleX = Random.Range(minScale.x, maxScale.x);
        float randomScaleY = Random.Range(minScale.y, maxScale.y);
        newObstacle.transform.localScale = new Vector3(randomScaleX, randomScaleY, 1f);

        //다음 생성 위치 업데이트(일정 간격 유지)
        spawnGapX = Random.Range(5, 10); // 새로운 랜덤 간격 적용
        lastSpawnX += spawnGapX;

        if (lastSpawnX > 100f)
        {
            lastSpawnX = spawnStartX;
        }

    }


}
