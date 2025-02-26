using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            ItemInstance item = collision.GetComponent<ItemInstance>();
            if (item is ITakeable)
            {
                ITakeable itemTake = (ITakeable)item; 
                itemTake.Take();
            }

            //collision.GetComponent<ItemInstance>().Take(ResourceController resourceController);
            Destroy(collision.gameObject);
        }
    }
}
