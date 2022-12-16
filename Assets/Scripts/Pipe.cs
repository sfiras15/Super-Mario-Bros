using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Pipe : MonoBehaviour
{
    public Transform connection;
    public Vector3 enterDirection = Vector3.down;
    public Vector3 exitDirection = Vector3.zero;
    public KeyCode enterKeyCode = KeyCode.S;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (connection !=null && collision.CompareTag("Player"))
        {
            if (Input.GetKey(enterKeyCode))
            {
                StartCoroutine(Enter(collision.transform));
            }
        }
    }
    private IEnumerator Enter(Transform player)
    {
        player.GetComponent<PlayerMouvement>().enabled = false;
        Vector3 enteredPosition = transform.position + enterDirection;
        Vector3 enteredScale = Vector3.one * 0.5f;
        yield return Move(player, enteredPosition, enteredScale);
        // Exit part 
        yield return new WaitForSeconds(0.5f);

        if (exitDirection != Vector3.zero)
        {
            player.position = connection.position - exitDirection;
            yield return Move(player, connection.position + exitDirection, Vector3.one);
        }
        else
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
        }
        player.GetComponent<PlayerMouvement>().enabled = true;
    }

    private IEnumerator Move(Transform player, Vector3 endPosition,Vector3 endScale)
    {
        float elapsed = 0f;
        float duration = 0.5f;
        
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            player.position = Vector3.Lerp(player.position, endPosition, t);
            player.localScale = Vector3.Lerp(player.localScale, endScale, t);
            elapsed += 0.005f; // Time.deltaTime was too slow here

            yield return null;

        }
        player.position = endPosition;
        player.localScale = endScale;
    }
}
