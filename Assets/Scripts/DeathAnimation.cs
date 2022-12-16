using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    public Sprite deathSprite;
    SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        if (gameObject.CompareTag("Player"))
        {
            spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        }
        else
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        
        UpdateSprite();
        UpdatePhysics();
        StartCoroutine(nameof(Animate));
    }
    public void UpdateSprite()// changes the spriteRenderer of the gameobject attached to it  
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10;
        if (spriteRenderer.sprite != deathSprite)
        {
            spriteRenderer.sprite = deathSprite;
        }
    }
    public void UpdatePhysics()// disables  the colliders/ makes the rigidbody kinematic of the gameobject attached to it 
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach(Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
        GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        //PlayerMouvement playerMouvement = GetComponent<PlayerMouvement>();
        EntityAnimation entityAnimation = GetComponent<EntityAnimation>();
        //if (playerMouvement != null)
        //{
        //    playerMouvement.enabled = false;
        //}
        if (entityAnimation != null)
        {
            entityAnimation.enabled = false;
        }
    }
    public IEnumerator Animate()
    {        
        float jumpForce = 10f;
        Vector3 velocity = Vector3.up * jumpForce;
        float gravity = -9.8f;
        float elapsedTime = 0f;
        float duration = 3f;
        if (elapsedTime < duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
