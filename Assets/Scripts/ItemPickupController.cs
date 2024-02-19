using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickupController : MonoBehaviour
{
    public enum ItemPickup
    {
        BlastRadius,
        Speed,
        ExtraBomb
    }

    public ItemPickup type;

    private void OnItemPickup(GameObject player)
    {
        switch (type)
        {
            case ItemPickup.BlastRadius:
                player.GetComponent<BombController>().explosionRadius += 1;
                break;
            case ItemPickup.Speed:
                player.GetComponent<MovementController>().speed += 1;
                break;
            case ItemPickup.ExtraBomb:
                player.GetComponent<BombController>().AddBomb();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnItemPickup(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
