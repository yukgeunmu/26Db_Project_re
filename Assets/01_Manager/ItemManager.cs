using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemManager : MonoBehaviour
{
    private PlayerController player;
    public LayerMask Obstacle; // ��ֹ� ���̾� ����

    [Header("������ ������Ʈ")]
    public GameObject[] item; //

    public float spawnStart_x = 0; // X�� ���� ��ġ
    public float spawnStart_y = -3f;
    private float spawnGapX;
    private float lastSpawn_x;
    private float spawnInterval;
    private float speedFactor;
    public float minSpawnInterval = 0.3f; // �ּ� ���� ���� (�ʹ� ������ �ʵ��� ����)
    [SerializeField][Range(0.1f,10f)]private float itemCreateSpeed = 1f;


    private void Start()
    {
        lastSpawn_x = spawnStart_x;
        StartCoroutine(SpawnItemRoutine());
    }

    private IEnumerator SpawnItemRoutine()
    {
        while (true)
        {
            SpawnItem();
            spawnGapX = GameManager.Instance.ItemGap;
            spawnInterval = GameManager.Instance.ItemSpwnTime;
            speedFactor = GameManager.Instance.ItemSpawnFactor;
            float adjustedInterval = Mathf.Max(minSpawnInterval, spawnInterval - speedFactor);
            yield return new WaitForSeconds(itemCreateSpeed);
        }
    }

    public void SpawnItem()
    {
        GameObject newObject = null;
        Transform newTrans = null;

        int number = Random.Range(1,100);

        float adjustedY = Random.Range(-3, 1);
        Vector3 spawnPosition = new Vector3(lastSpawn_x, adjustedY, 0);
        
        if(number <= 70) newObject = Instantiate(item[0], spawnPosition, Quaternion.identity);
        else if(number <= 90) newObject = Instantiate(item[1], spawnPosition, Quaternion.identity);
        else if(number <= 100 ) newObject = Instantiate(item[2], spawnPosition, Quaternion.identity);

        newTrans = newObject.transform;
        newTrans.parent = this.transform;


        lastSpawn_x += spawnGapX;

        if (lastSpawn_x > 20f)
        {
            lastSpawn_x = spawnStart_x;
        }

    }

    //private float GetItemHeight(float xPosition)
    //{

    //    RaycastHit2D hit = Physics2D.Raycast(new Vector2(xPosition,spawnStart_y), Vector2.up, Mathf.Infinity, Obstacle);
    //    if (hit.collider != null)
    //    {
    //        int add = Random.Range(5, 7);
    //        Debug.Log("��ֹ� �߰�! Y ����");
    //        Debug.Log(hit.point.y);
    //        return hit.point.y + add;
    //    }
    //    return spawnStart_y; // ��ֹ��� ������ �⺻ Y�� ��ȯ
    //}

}

