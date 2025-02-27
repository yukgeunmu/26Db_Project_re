using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    [SerializeField] private AudioClip collisionClip;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.resourceController.CurrentHealth >= 0)
            {
                Debug.Log("�浹��");
                GameManager.Instance.resourceController.ChangeHealth(-10f);
                AudioManager.PlayClip(collisionClip);
            }
        }
    }

}
