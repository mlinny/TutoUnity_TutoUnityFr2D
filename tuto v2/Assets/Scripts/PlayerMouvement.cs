using UnityEngine;


public class PlayerMouvement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private bool isJumping;
    private bool isGrounded;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public Animator animator;
    public SpriteRenderer spriteRenderer;


    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        // La position au sol est déterminé par une ligne entre ces 2 positions, qui sont 
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }
        
        MovePlayer(horizontalMovement);

        float vitesse = Mathf.Abs(rb.velocity.x);
        FlipPlayer(rb.velocity.x);
        animator.SetFloat("Speed", vitesse);
    
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


}
