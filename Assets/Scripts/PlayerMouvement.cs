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
    // Variables for clamping the camera
    private float leftEdge;
    private float halfCharWidth;
    // Variables to determine the jumping force of mario & the custom gravity for this game
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float JumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);

    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f, 2);

    // booleans for differents states of mario
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
        // check whether the player is on the ground or not
        grounded = rb2D.Raycast(Vector2.down);
        if (grounded)
        {
            GroundedMouvement();
        }
        ApplyGravity();

    }
    private void ApplyGravity()
    {
        // the more the player  hold on to the jump button the higher mario goes
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;
        velocity.y += gravity * multiplier * Time.deltaTime;
        // to cap the value of velocity.y
        velocity.y = Mathf.Max(gravity / 2f, velocity.y);
    }
    private void GroundedMouvement()
    {
        // to make sure the value of velocity.y is always close to 0
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
        // The more the player keeps going left or right the faster he becomes
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
        // mario can't go the left edge of the screen
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
            // if mario is going down on an enemy
            if (transform.DotTest(collision.transform, Vector2.down))
            {
                velocity.y = JumpForce / 2f + 2f;
                jumping = true;
            }
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            // if mario is hitting the block from below
            if (transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
        }
    }

}
