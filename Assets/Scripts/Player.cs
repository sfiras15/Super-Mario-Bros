using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AnimatedSpriteRenderer smallRenderer;
    public AnimatedSpriteRenderer bigRenderer;
    public AnimatedSpriteRenderer activeRenderer;
    public DeathAnimation deathAnimation;
    CapsuleCollider2D capsuleCollider;

    public bool small => smallRenderer.enabled;
    public bool death => deathAnimation.enabled;
    public bool isStar;
    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeRenderer = smallRenderer;
    }
    public void Hit()
    {
        if (!small)
        {
            Shrink();
        }
        else
        {
            Death();
        }
    }

    public void Death()
    { 
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        // deathAnimation script will re-activate the sprite Renderer for the small mario
        deathAnimation.enabled = true;
        GameManager.Instance.ResetLevel(3f);

    }
    private void Shrink()
    {
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRenderer = smallRenderer;
        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = Vector2.zero;
        StartCoroutine(Animate());
    }
    public void Grow()
    { 
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRenderer = bigRenderer;
        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);
        StartCoroutine(Animate());

    }
    private IEnumerator Animate()
    {
        float elapsed = 0f;
        float duration = 0.75f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled; 
            }
            yield return null;
        }
        bigRenderer.enabled = false;
        smallRenderer.enabled = false;
        activeRenderer.enabled = true;
    }
    public void StarPower()
    {
        StartCoroutine(StarAnimation());
    }
    private IEnumerator StarAnimation()
    {
        isStar = true;
        float elapsed = 0f;
        float duration = 10f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if (Time.frameCount % 4 == 0)
            {
                activeRenderer.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if (Time.frameCount % 4 == 1)
            {
                activeRenderer.GetComponent<SpriteRenderer>().color = Color.green;
            }
            yield return null;
        }
        // Otherwise the colors for the active renderer will persist in the last second
        smallRenderer.GetComponent<SpriteRenderer>().color = Color.white;
        bigRenderer.GetComponent<SpriteRenderer>().color = Color.white;
        isStar = false;
    }
}
