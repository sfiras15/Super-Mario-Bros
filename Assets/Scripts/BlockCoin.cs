using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCoin: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.AddCoin();
        StartCoroutine(Animate());
    }
    // The coin will go up from the mystery box then comes back down
    private IEnumerator Animate()
    {
        Vector3 startingPosition = transform.localPosition;
        Vector3 animatedPosition = startingPosition + Vector3.up * 2f;
        yield return Move(startingPosition, animatedPosition);
        yield return Move(animatedPosition, startingPosition);
        Destroy(gameObject);
    }
    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsedTime = 0f;
        float duration = 0.25f;
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
