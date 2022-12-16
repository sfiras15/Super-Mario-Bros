using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shell;
    public float shellSpeed = 12f;
    SpriteRenderer spriteRenderer;
    bool isShell;
    bool isPushed;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (!isShell && collision.gameObject.CompareTag("Player"))
        {
            if (player.isStar)
            {
                GetComponent<DeathAnimation>().enabled = true;
                Destroy(gameObject, 3f);
            }
            else
            {
                if (transform.DotTest(collision.transform, Vector2.up))
                {
                    EnterShell();
                }
                else
                {
                    player.Hit();
                }
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (isShell && collision.CompareTag("Player"))
        {
           if (!isPushed)
           {
                Vector2 direction = new Vector2(transform.position.x - collision.transform.position.x, 0);
                PushShell(direction.normalized);
           }
           else
           {
                if (player.isStar)
                {
                    GetComponent<DeathAnimation>().enabled = true;
                    Destroy(gameObject, 3f);
                }
                else
                {
                    player.Hit();
                }
                
           }
            
        }
    }
    private void PushShell(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        EntityMouvement mouvement = GetComponent<EntityMouvement>();
        mouvement.enabled = true;
        mouvement.speed = shellSpeed;
        mouvement.direction = direction;
        isPushed = true;
        gameObject.layer = LayerMask.NameToLayer("Shell");

    }
    private void EnterShell()
    {
        isShell = true;
        GetComponent<EntityAnimation>().enabled = false;
        GetComponent<EntityMouvement>().enabled = false;
        spriteRenderer.sprite = shell;
        GetComponent<DeathAnimation>().deathSprite = shell;

    }
}
