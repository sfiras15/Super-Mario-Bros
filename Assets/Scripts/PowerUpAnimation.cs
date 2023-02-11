using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();
        CircleCollider2D physicsCollider = GetComponent<CircleCollider2D>();
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        EntityMouvement entityMouvement = GetComponent<EntityMouvement>();
        spriteRenderer.enabled = false;
        triggerCollider.enabled = false;
        physicsCollider.enabled = false;
        rb2D.isKinematic = true;
        entityMouvement.enabled = false;
        // The animation of the object should only play after the animation of the block itself ends
        yield return new WaitForSeconds(0.25f);

        spriteRenderer.enabled = true;

        Vector3 startingPosition = transform.localPosition;
        Vector3 animatedPosition = startingPosition + Vector3.up;

        float elapsedTime = 0f;
        float duration = 0.5f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.localPosition = Vector3.Lerp(startingPosition, animatedPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = animatedPosition;
        triggerCollider.enabled = true;
        physicsCollider.enabled = true;
        rb2D.isKinematic = false;
        entityMouvement.enabled = true;
    }
    
}
