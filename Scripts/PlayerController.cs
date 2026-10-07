using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd;

    public float speed = 5f;
    public float jumpForce = 7f;
    private Animator animator;
    private bool isGrounded;
    private bool facingRight = false;

    // Start se ejecuta una vez al iniciar
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    // Update se ejecuta cada frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        float speedAnimation = Mathf.Abs(move);

        animator.SetFloat("Speed", speedAnimation);

        // Movimiento horizontal
        rd.velocity = new Vector2(move * speed, rd.velocity.y);

        //voltear el personaje
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

        // Saltar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rd.velocity = new Vector2(rd.velocity.x, 0f);
            rd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            isGrounded = false;
            animator.SetBool("isJump", true);
        }
    }

    // Detectar cuando toca el suelo
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJump", false);
        }
    }

    // Detectar cuando deja de tocar el suelo
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("isJump", true);
        }
    }

    // Método para voltear el personaje
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}