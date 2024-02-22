using Photon.Pun;
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
    public int explosionRadius = 1;

    [Header("Destructible")]
    public GameObject destructiblePrefabs;
    public Tilemap destructibleTilemaps;
    public float duration = 1f;

    private PhotonView view;
    private void Awake()
    {
        destructibleTilemaps = GameObject.Find("Destructibles").GetComponent<Tilemap>();
        view = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        bombRemaining = bombAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (bombRemaining > 0 && Input.GetKeyDown(inputKey))
            {
                StartCoroutine(PlaceBomb());
            }
        }
    }

    IEnumerator PlaceBomb()
    {
        var position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        var bomb = PhotonNetwork.Instantiate(bombPrefab.name, position, Quaternion.identity);
        bombRemaining--;

        yield return new WaitForSeconds(bombFuseTime);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        var explosion = PhotonNetwork.Instantiate(explosionPrefab.name, position, Quaternion.identity);
        var pv = explosion.GetComponent<PhotonView>();
        if (pv)
        {
            pv.RPC("SetExplosion", RpcTarget.All, "start", null);
        }

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        PhotonNetwork.Destroy(bomb);
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

        var explosion = PhotonNetwork.Instantiate(explosionPrefab.name, position, Quaternion.identity);
        var pv = explosion.GetComponent<PhotonView>();
        if (pv)
        {
            pv.RPC("SetExplosion", RpcTarget.All, length > 1 ? "middle" : "end", direction);
        }

        Explode(position, direction, --length);
    }

    void DestroyWall(Vector2 position)
    {
        var tilePos = destructibleTilemaps.WorldToCell(position);
        var tile = destructibleTilemaps.GetTile(tilePos);

        if (tile != null)
        {
            var desObj = PhotonNetwork.Instantiate(destructiblePrefabs.name, position, Quaternion.identity);
            view.RPC(nameof(RemoveTile_RPC), RpcTarget.AllBuffered, position);
        }
    }

    [PunRPC]
    void RemoveTile_RPC(Vector2 position)
    {
        var tilePos = destructibleTilemaps.WorldToCell(position);
        destructibleTilemaps.SetTile(tilePos, null);
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
