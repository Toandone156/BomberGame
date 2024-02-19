using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3f;
    public int bombAmount = 1;

    private int bombRemaining;

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayer;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("Destructible")]
    public GameObject destructiblePrefabs;
    public Tilemap destructibleTilemaps;
    public float duration = 1f;

    private void OnEnable()
    {
        bombRemaining = bombAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (bombRemaining > 0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(PlaceBomb());
        }
    }

    IEnumerator PlaceBomb()
    {
        var position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        var bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bombRemaining--;

        yield return new WaitForSeconds(bombFuseTime);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        var explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveSprite(explosion.startSprite);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Destroy(bomb);
        bombRemaining++;
    }

    void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0) return;

        position += direction;

        var IsStageWall = Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayer);
        if (IsStageWall)
        {
            DestroyWall(position);
            return;
        }

        var explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveSprite(length > 1 ? explosion.middleSprite : explosion.endSprite);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, direction, --length);
    }

    void DestroyWall(Vector2 position)
    {
        var tilePos = destructibleTilemaps.WorldToCell(position);
        var tile = destructibleTilemaps.GetTile(tilePos);

        if (tile != null)
        {
            var desObj = Instantiate(destructiblePrefabs, position, Quaternion.identity);
            Destroy(desObj, duration);
            destructibleTilemaps.SetTile(tilePos, null);
        }
    }

    void AddItemPickup(Vector2 position)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            collision.isTrigger = false;
        }
    }

    public void AddBomb()
    {
        bombAmount++;
        bombRemaining++;
    }
}
