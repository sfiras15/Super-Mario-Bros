using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    // there are death barriers between the gaps of the stage 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<Player>().Death();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
