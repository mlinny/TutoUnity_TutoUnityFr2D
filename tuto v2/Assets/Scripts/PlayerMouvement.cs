using UnityEngine;


public class PlayerMouvement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private bool isJumping;
    private bool isGrounded;

    public Transform groundCheck;
    public float groundCheckRadius = 2;
    public LayerMask collisionLayers;

    public Animator animator;
    public SpriteRenderer spriteRenderer;


    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    

    void Update()
    {
        // Update doit s'occuper de tous les calculs, l'impact graphique se fera dans FixedUpdate
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

        FlipPlayer(rb.velocity.x);

        float vitesse = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", vitesse);

    }

    void CheckPlayerEstAuSol()
    { 
            // La position au sol est déterminé par une ligne entre ces 2 positions, qui sont 
        isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, collisionLayers); 
    }


    void FixedUpdate()
    {
        //La gestion du mouvement doit se faire dans FixedUpdate, sinon il y aura des latences
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        
        CheckPlayerEstAuSol();

        MovePlayer(horizontalMovement);

    }

    void FlipPlayer(float _vitesse)
    {
        
        if (_vitesse > 0.1F)
        {
            spriteRenderer.flipX = false;
        }
        else if (_vitesse < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }


    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);

        if (isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //permet de tracer un cercle vide de centre et et de rayon
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }


}
