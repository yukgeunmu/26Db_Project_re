using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [Header("땅 프리팹")]
    public GameObject groundPrefab; // 생성할 땅 프리팹
    public int poolSize = 5; // 오브젝트 풀 크기
    public float spawnX = 10f; // 처음 생성될 위치
    public float moveSpeed = 2.5f; // 땅 이동 속도
    public float despawnX = -15f; // 화면 밖으로 나가면 위치 초기화할 기준
    public float groundWidth = 5f; // 땅 간 간격

    private List<GameObject> groundPool = new List<GameObject>();

    void Start()
    {
        // 미리 땅 오브젝트를 생성하여 풀링
        for (int i = 0; i < poolSize; i++)
        {
            GameObject ground = Instantiate(groundPrefab, new Vector3(spawnX + i * groundWidth, -2.5f, 0), Quaternion.identity);
            ground.SetActive(true);
            groundPool.Add(ground);
        }
    }

    void Update()
    {
        MoveGround(); // 땅 이동 처리
    }

    private void MoveGround()
    {
        foreach (GameObject ground in groundPool)
        {
            // 왼쪽으로 이동
            ground.transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            // 땅이 화면 왼쪽 밖으로 나가면 오른쪽 끝으로 이동하여 재사용
            if (ground.transform.position.x <= despawnX)
            {
                float maxX = GetMaxX();
                ground.transform.position = new Vector3(maxX + groundWidth, ground.transform.position.y, ground.transform.position.z);
            }
        }
    }

    // 현재 땅 중 가장 오른쪽에 있는 땅의 X 좌표를 반환
    private float GetMaxX()
    {
        float maxX = despawnX;
        foreach (GameObject ground in groundPool)
        {
            if (ground.transform.position.x > maxX)
            {
                maxX = ground.transform.position.x;
            }
        }
        return maxX;
    }
}
