using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float highPosY = 1f; // 최대높이
    public float lowPosY = -1f; // 최저높이
    public float longPosX = 2f; // 최대길이
    public float shortPosX = 2f; // 최저길이
    public ResourceController resourceController;

    public Transform obstacle;

    /*public Vector2 SetRandomPlace(Vector2 lastPositon, int obstacleCount)
    {
        float PosX = Random.Range(longPosX, shortPosX); // longPos와 shortPos의 범위에서 랜덤값 부여
        float PosY = Random.Range(highPosY, lowPosY); // highPos와 lowPos의 범위에서 랜덤값 부여

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