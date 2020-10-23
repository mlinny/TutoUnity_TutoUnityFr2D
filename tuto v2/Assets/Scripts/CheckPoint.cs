using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    private Transform playerSpawnPos;
    private void Awake()
    {
        playerSpawnPos = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerSpawnPos.position = gameObject.transform.position;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;           
        }
    }
}
