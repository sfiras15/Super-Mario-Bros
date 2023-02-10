using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite stomp;
    SpriteRenderer spriteRenderer;
    public EntityMouvement mouvement;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player.isStar)
            {
                GetComponent<DeathAnimation>().enabled = true;
                Destroy(gameObject, 3f);
            }
            else
            {
                if (transform.DotTest(collision.transform, Vector2.up))
                {
                    spriteRenderer.sprite = stomp;
                    GetComponent<CircleCollider2D>().enabled = false;
                    GetComponent<EntityAnimation>().enabled = false;
                    mouvement.enabled = false;
                    Destroy(gameObject, 0.5f);
                }
                else
                {
                    player.Hit();
                }
            }
            
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            GetComponent<DeathAnimation>().enabled = true;
            Destroy(gameObject, 3f);
        }
    }
}
