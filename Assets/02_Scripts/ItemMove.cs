using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    private float speed = 1f;
    public AudioClip itemClip;

    // Update is called once per frame
    void Update()
    {
        speed = 1f;
        MoveElements(speed);
    }

    private void MoveElements(float speed)
    {
        this.transform.position += Vector3.left * speed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            AudioManager.PlayClip(itemClip);
            
        }
    }

}
