using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private CapsuleCollider2D capsuleCollider;
    public float moveSpeed = 5f;

    public Vector3 velocity;
    public float inputAxis;
    private float leftEdge;
    private float halfCharWidth;

    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;

    public float JumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f, 2);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }

    public GameManager gameManager;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        halfCharWidth = capsuleCollider.size.x / 2;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        HorizontalMouvement();
        grounded = rb2D.Raycast(Vector2.down); //reread this part to understand it 
        if (grounded)
        {
            GroundedMouvement();
        }
        ApplyGravity();

    }
    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(gravity / 2f, velocity.y);
    }
    private void GroundedMouvement()
    {
        velocity.y = Mathf.Max(0f, velocity.y);
        jumping = velocity.y > 0f;
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = JumpForce;
            jumping = true;

        }
    }
    private void HorizontalMouvement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);

        if (rb2D.Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0f;
        }
        if (velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }
        else if (velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        Vector3 position = rb2D.position;
        position += velocity * Time.fixedDeltaTime;
        
        Vector3 cameraMinBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z));
   
        leftEdge = cameraMinBounds.x;
        //position.x = Mathf.Clamp(position.x, leftEdge + halfCharWidth, rightEdge - halfCharWidth);
        if (position.x - halfCharWidth <= leftEdge)
        {
            position.x = leftEdge + halfCharWidth;
        }
        rb2D.MovePosition(position);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (collision.transform.DotTest(transform, Vector2.up))
            {
                velocity.y = JumpForce / 2f + 2f;
                jumping = true;
            }
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if (transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
        }
    }

}
