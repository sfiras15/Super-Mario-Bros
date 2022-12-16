using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimation : MonoBehaviour
{
    public float animationTime = 0.25f;
    private int currentFrame = 0;
    public Sprite[] walkingSprites;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2D;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), animationTime, animationTime);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        currentFrame++;
        if (currentFrame >= walkingSprites.Length)
        {
            currentFrame = 0;
        }
        if (currentFrame >= 0 && currentFrame < walkingSprites.Length)
        {
            spriteRenderer.sprite = walkingSprites[currentFrame];
        }
    }
    
}
