using UnityEngine;

public class ItemPickupController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
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
            audioSource.Play();
            OnItemPickup(collision.gameObject);
            Destroy(gameObject, 0.1f);
        }
    }

    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {

    //    }
    //}
}
