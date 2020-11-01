using System;
using UnityEngine;


public class PlayerMouvement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private bool isJumping;
    private bool isGrounded;
    [HideInInspector]
    public bool isClimbing;

    public Transform groundCheck;
    public float groundCheckRadius = 2;
    public LayerMask collisionLayers;

    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D playerCollider;

    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    public static PlayerMouvement instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMouvement dans la scène");
            return;
        }
        instance = this;
    }

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
        animator.SetBool("IsClimbing", isClimbing);

    }

    void CheckPlayerEstAuSol()
    {
        // La position au sol est déterminé par une ligne entre ces 2 positions, qui sont 
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
    }


    void FixedUpdate()
    {
        //La gestion du mouvement doit se faire dans FixedUpdate, sinon il y aura des latences
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float verticalMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        CheckPlayerEstAuSol();

        MovePlayer(horizontalMovement, verticalMovement );

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


    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {

        if (!isClimbing)
        {
            PerformDeplacementHorizontal(_horizontalMovement);
        }
        else
        {
            PerformDeplacementVertical(_verticalMovement );
        }

    }

    private void PerformDeplacementVertical(float verticalMovement)
    {
        // le 0 permet de s'accrocher à l'échelle
        Vector3 targetVelocity = new Vector2( 0, verticalMovement);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
    }

    private void PerformDeplacementHorizontal(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);

        if (isJumping)
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
