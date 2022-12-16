using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class EntityMouvement : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public Vector2 direction = new Vector2(-1,0);
    public float speed = 2f;
    public Vector2 velocity;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        enabled = false;
    }
    private void OnBecameVisible()
    {
        enabled = true;
    }
    private void OnBecameInvisible()
    {
        enabled = false;
    }
    private void OnEnable()
    {
        rb2D.WakeUp();
    }
    private void OnDisable()
    {
        rb2D.Sleep();
    }
    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
        if (rb2D.Raycast(direction))
        {
            direction = -direction;
        }
        if (rb2D.Raycast(Vector2.down))
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
        if (direction.x > 0f)
        {
            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if (direction.x < 0f)
        {
            transform.localEulerAngles = Vector3.zero;
        }
    }
}
