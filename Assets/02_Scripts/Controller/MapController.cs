using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapLooper : MonoBehaviour
{
    public Transform tilemap1; // 첫 번째 타일맵
    public Transform tilemap2; // 두 번째 타일맵
    public float moveSpeed = 2.5f; // 타일맵 이동 속도
    private float resetPositionX = -17.8f; // 타일이 사라지는 위치
    private float startPositionX = 17.98f; // 타일이 다시 나타나는 위치

    void Start()
    {
        // ✅ **타일맵이 비활성화되어 있으면 활성화**
        if (!tilemap1.gameObject.activeSelf)
        {
            tilemap1.gameObject.SetActive(true);
        }
        if (!tilemap2.gameObject.activeSelf)
        {
            tilemap2.gameObject.SetActive(true);
        }

        // 첫 번째 타일맵은 (0, 0), 두 번째 타일맵은 (17.98, 0)에 배치
        tilemap1.position = new Vector3(0, tilemap1.position.y, tilemap1.position.z);
        tilemap2.position = new Vector3(startPositionX, tilemap2.position.y, tilemap2.position.z);
    }

    void Update()
    {
        MoveTilemaps();
    }

    private void MoveTilemaps()
    {
        // ✅ **이동 전, 타일맵이 비활성화되었으면 다시 활성화**
        if (!tilemap1.gameObject.activeSelf)
        {
            tilemap1.gameObject.SetActive(true);
        }
        if (!tilemap2.gameObject.activeSelf)
        {
            tilemap2.gameObject.SetActive(true);
        }

        // 타일맵을 왼쪽으로 이동
        tilemap1.position += Vector3.left * moveSpeed * Time.deltaTime;
        tilemap2.position += Vector3.left * moveSpeed * Time.deltaTime;

        // 첫 번째 타일맵이 resetPositionX에 도달하면 다시 startPositionX로 이동
        if (tilemap1.position.x <= resetPositionX)
        {
            tilemap1.position = new Vector3(startPositionX, tilemap1.position.y, tilemap1.position.z);
        }

        // 두 번째 타일맵이 resetPositionX에 도달하면 다시 startPositionX로 이동
        if (tilemap2.position.x <= resetPositionX)
        {
            tilemap2.position = new Vector3(startPositionX, tilemap2.position.y, tilemap2.position.z);
        }
    }
}
