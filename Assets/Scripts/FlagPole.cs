using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    // speed of the levelCompletion animation
    float speed = 6f;
    // Bottom of the flagPole
    public Transform bottomPosition;
    public Transform flag;
    public Transform castle;
    public int nextWorld = 1;
    public int nextStage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Move(flag, bottomPosition.position));
            StartCoroutine(LevelCompleteSequence(collision.transform));
        }   
    }
    private IEnumerator LevelCompleteSequence(Transform player)
    {
        player.GetComponent<PlayerMouvement>().enabled = false;
        yield return Move(player,bottomPosition.position);
        yield return Move(player,player.position + Vector3.right);
        yield return Move(player,player.position + Vector3.right + Vector3.down);
        player.gameObject.GetComponentInChildren<AnimatedSpriteRenderer>().flagAnimation = true;
        yield return Move(player,castle.position);
        player.gameObject.SetActive(false);
        GameManager.Instance.LoadLevel(nextWorld, nextStage);
    }
    private IEnumerator Move(Transform subject ,Vector3 endPosition)
    {
        while (Vector3.Distance(subject.position,endPosition) > 0.125f)
        {
            subject.position = Vector3.MoveTowards(subject.position, endPosition, speed * Time.deltaTime);
            yield return null;
        }
        subject.position = endPosition;
    }
}
