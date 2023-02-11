using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shell;
    // the speed of koopa in shell mode 
    public float shellSpeed = 12f;
    SpriteRenderer spriteRenderer;
    bool isShell;
    bool isPushed;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Koopa have 2 colliders : - Cercle collider for his alive state // - Box collider set as trigger for his shell state
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
                // If mario is landing on him from above
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
        // Make him move in the push direction really fast
        EntityMouvement mouvement = GetComponent<EntityMouvement>();
        mouvement.enabled = true;
        mouvement.speed = shellSpeed;
        mouvement.direction = direction;
        
        isPushed = true;
        // For Goomba's collision detection
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
