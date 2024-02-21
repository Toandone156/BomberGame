using Photon.Pun;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float timeDuration = 1f;

    [Range(0f, 1f)]
    public float spawnChange = 0.5f;

    public GameObject[] spawnItems;

    void Start()
    {
        Destroy(this.gameObject, timeDuration);
    }

    private void OnDestroy()
    {
        if(spawnItems.Length > 0 && Random.value <= spawnChange)
        {
            var itemIndex = Random.Range(0, spawnItems.Length - 1);
            PhotonNetwork.Instantiate(spawnItems[itemIndex].name, transform.position, Quaternion.identity);
        }
    }
}
