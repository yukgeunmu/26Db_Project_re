using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float highPosY = 1f; // �ִ����
    public float lowPosY = -1f; // ��������
    public float longPosX = 2f; // �ִ����
    public float shortPosX = 2f; // ��������
    public ResourceController resourceController;

    public Transform obstacle;

    /*public Vector2 SetRandomPlace(Vector2 lastPositon, int obstacleCount)
    {
        float PosX = Random.Range(longPosX, shortPosX); // longPos�� shortPos�� �������� ������ �ο�
        float PosY = Random.Range(highPosY, lowPosY); // highPos�� lowPos�� �������� ������ �ο�

        Vector2 placePosition = lastPositon + new Vector2(PosX, PosY);
        
        transform.position = placePosition;

        return placePosition;
    }*/


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { 
            resourceController = GetComponent<ResourceController>();

            if (resourceController.CurrentHealth >= 0)
            {
                resourceController.ChangeHealth(-10);
            }
        }
    }
}