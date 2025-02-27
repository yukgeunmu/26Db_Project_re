using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 아이템의 Take() 함수 호출 후 파괴
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
