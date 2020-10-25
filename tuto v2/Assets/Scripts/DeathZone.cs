using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    private Transform playerSpawn;
    private Animator fadeSystem;
    private int deathZoneDamage = 10;

    private void Awake()
    {
        Debug.Log("Chargement du playerSpawn");
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        Debug.Log("Chargement du FadeSystem");
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(GetBackToSpawn(collision));

        }
    }

    private void PerformDamage(Collider2D collision)
    {
        PlayerHealth health = collision.transform.GetComponent<PlayerHealth>();
        health.TakeDamage(deathZoneDamage);
    }


    private IEnumerator GetBackToSpawn(Collider2D collision)
    {

        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        collision.transform.position = playerSpawn.position;
        PerformDamage(collision);


    }




}
