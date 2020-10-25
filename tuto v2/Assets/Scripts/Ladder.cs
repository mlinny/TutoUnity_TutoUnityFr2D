using System.Runtime.InteropServices;
using UnityEngine;

public class Ladder : MonoBehaviour
{
//    [HideInInspector]
    public bool isInRange;
    private PlayerMouvement playerMouvement;
    public BoxCollider2D topCollider;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerMouvement = player.GetComponent<PlayerMouvement>();        
    }

    private void Update()
    {

        if (isInRange && playerMouvement.isClimbing && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            //Debug.Log("Sortie de l'échelle");
            playerMouvement.isClimbing = false;
            topCollider.isTrigger = false;
        }

        if (isInRange   && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)))
        //if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("Montéede l'échelle");
            playerMouvement.isClimbing = true;
            topCollider.isTrigger = true;            
            return;
        }
        
        
    }

    // Méthode de collision avec l'environnement
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopClimbing();
        }
    }

    private void StopClimbing()
    {
        isInRange = false;
        playerMouvement.isClimbing = false;
        topCollider.isTrigger = false;
        
    }
}
