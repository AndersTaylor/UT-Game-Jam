using UnityEngine;

public class PlatformerCharacterController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    Rigidbody2D rb;
    bool isGrounded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // checks if player is on ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        Debug.Log(isGrounded);
    }

    void FixedUpdate()
    {
        // horizontal movemnet
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, rb.linearVelocityY);
        rb.linearVelocity = move;

        // jumping 
        if (Input.GetAxisRaw("Jump") > 0 && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
        }
    }
}
