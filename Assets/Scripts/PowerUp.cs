using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Power
    {
        MagicMushroom,
        ExtraLife,
        Starpower,
        Coin
    }
    public Power power;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect(collision.gameObject);
        }
    }
    private void Collect(GameObject player)
    {
        switch (power)
        {
            case Power.MagicMushroom:
                player.GetComponent<Player>().Grow();
                break;
            case Power.ExtraLife:
                GameManager.Instance.AddLife();
                break;
            case Power.Starpower:
                player.GetComponent<Player>().StarPower();
                break;
            case Power.Coin:
                GameManager.Instance.AddCoin();
                break;
        }
        Destroy(gameObject);
    }
}
