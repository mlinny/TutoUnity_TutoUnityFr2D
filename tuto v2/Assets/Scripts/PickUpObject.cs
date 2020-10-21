﻿using UnityEngine;

public class PickUpObject : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            Inventory.instance.AddCoin();
        }
    }


}
