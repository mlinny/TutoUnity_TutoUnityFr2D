using UnityEngine;

public class EnnemiPatrol : MonoBehaviour
{

    public float speed;
    public Transform[] waypoints;
    private Transform target;
    private int destPoint = 0;
    public int damageCollision = 20;
    
    public SpriteRenderer graphics;


    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
    } 

    // Update is called once per frame
    void Update()
    {
        
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        //si l'ennemie est quasiment arrivé à sa destination
        if(Vector3.Distance(transform.position, target.position ) < 0.3f)
        {
            // '%' => reste de la division
            destPoint = (destPoint + 1) % waypoints.Length; //recherche du point suivant
            target = waypoints[destPoint]; // affectation de la nouvelle target
            graphics.flipX = !graphics.flipX;        
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth health = collision.transform.GetComponent<PlayerHealth>();
            health.TakeDamage(damageCollision);
        }
    }


}
