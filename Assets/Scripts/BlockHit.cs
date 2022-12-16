using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class BlockHit : MonoBehaviour
{
    public int maxHits = -1;
    public Sprite emptyBlock;
    private bool animating;
    public GameObject items;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!animating && collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.DotTest(transform, Vector2.up))
            {
                Hit();
            }
        }
    }
    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        maxHits--;
        if (spriteRenderer.sprite != emptyBlock && items != null)
        {
            Instantiate(items, transform.position, Quaternion.identity);
        }
        if (maxHits == 0)
        {   
            spriteRenderer.enabled = true;
            spriteRenderer.sprite = emptyBlock;
        }
        StartCoroutine(Animate());

    }
    private IEnumerator Animate()
    {
        animating = true;
        Vector3 startingPosition = transform.localPosition;
        Vector3 animatedPosition = startingPosition + Vector3.up * 0.5f;
        yield return Move(startingPosition, animatedPosition);
        yield return Move(animatedPosition, startingPosition);
        animating = false;
    }
    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsedTime = 0f;
        float duration = 0.125f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = to;
    }
}