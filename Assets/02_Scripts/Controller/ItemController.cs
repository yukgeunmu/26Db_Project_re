using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� �������� Take() �Լ� ȣ�� �� �ı�
        if (collision.gameObject.CompareTag("Item"))
        {
            ItemInstance item = collision.GetComponent<ItemInstance>();
            if (item is ITakeable itemTake)
            {
                itemTake.Take();
            }

            Destroy(collision.gameObject);
        }
    }
}
