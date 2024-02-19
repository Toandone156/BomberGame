using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float timeDuration = 1f;

    [Range(0f, 1f)]
    public float spawnChange = 0.5f;

    public GameObject[] spawnItems;

    void Start()
    {
        Destroy(gameObject, timeDuration);
    }

    private void OnDestroy()
    {
        if(spawnItems.Length > 0 && Random.value <= spawnChange)
        {
            var itemIndex = Random.Range(0, spawnItems.Length - 1);
            Instantiate(spawnItems[itemIndex], transform.position, Quaternion.identity);
        }
    }
}
