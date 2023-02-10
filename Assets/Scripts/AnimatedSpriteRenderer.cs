using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    // Sprites to the differents states of mario
    public Sprite[] runningSprites;
    public Sprite idleSprite;
    public Sprite jumpSprite;
    public Sprite slideSprite;
    
    int currentFrame = 0;
    public bool idle;
    public bool sliding;
    public bool flagAnimation;
    public float animationTime = 1f / 6f;
    SpriteRenderer spriteRenderer;
    private PlayerMouvement playerMouvement;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMouvement = GetComponentInParent<PlayerMouvement>(); 
    }
    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }
    private void OnDisable()
    {
        spriteRenderer.enabled = false;
        //CancelInvoke();
        
    }
    private void Start()
    {
        InvokeRepeating(nameof(Animate), animationTime, animationTime);
    }
    private void Animate()
    {
        idle = playerMouvement.inputAxis == 0 && !playerMouvement.jumping;
        sliding = playerMouvement.inputAxis * playerMouvement.velocity.x < 0f;
        if (idle)
        {
            spriteRenderer.sprite = idleSprite;
            currentFrame = 0;
        }
        else if (playerMouvement.jumping)
        {
            spriteRenderer.sprite = jumpSprite;
        }
        else if (sliding)
        {
            spriteRenderer.sprite = slideSprite;
        }
        else
        {
            if (currentFrame >= runningSprites.Length)
            {
                currentFrame = 0;
            }
            if (currentFrame >= 0 && currentFrame < runningSprites.Length && playerMouvement.inputAxis != 0)
            {
                spriteRenderer.sprite = runningSprites[currentFrame];
                currentFrame++;
            }
        }
        if (flagAnimation)
        {
            if (currentFrame >= runningSprites.Length)
            {
                currentFrame = 0;
            }
            if (currentFrame >= 0 && currentFrame < runningSprites.Length)
            {
                spriteRenderer.sprite = runningSprites[currentFrame];
                currentFrame++;
            }
        }
       
        
        
    }
}
