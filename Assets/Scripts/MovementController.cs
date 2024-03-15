using Photon.Pun;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Vector2 direction = Vector2.zero;
    public float speed = 5f;

    public KeyCode left = KeyCode.A;
    public KeyCode down = KeyCode.S;
    public KeyCode right = KeyCode.D;
    public KeyCode up = KeyCode.W;

    public AnimationSpriteRender spriteRenderUp;
    public AnimationSpriteRender spriteRenderDown;
    public AnimationSpriteRender spriteRenderLeft;
    public AnimationSpriteRender spriteRenderRight;
    public AnimationSpriteRender spriteRenderDeath;
    private AnimationSpriteRender activeSpriteRender;

    private PhotonView view;
    private bool isDeath = false;

    [SerializeField] private AudioSource audioSource;

    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRender = spriteRenderDown;
        view = GetComponent<PhotonView>();
    }

    public void Update()
    {
        if (view.IsMine)
        {
            switch (true)
            {
                case var c when Input.GetKey(up):
                    view.RPC(nameof(SetDirection), RpcTarget.All, Vector2.up);
                    break;
                case var c when Input.GetKey(down):
                    view.RPC(nameof(SetDirection), RpcTarget.All, Vector2.down);
                    break;
                case var c when Input.GetKey(left):
                    view.RPC(nameof(SetDirection), RpcTarget.All, Vector2.left);
                    break;
                case var c when Input.GetKey(right):
                    view.RPC(nameof(SetDirection), RpcTarget.All, Vector2.right);
                    break;
                default:
                    view.RPC(nameof(SetDirection), RpcTarget.All, Vector2.zero);
                    break;
            }
        }
    }

    public void FixedUpdate()
    {
        var position = rigidbody.position;
        var translation = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }

    [PunRPC]
    private void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;

        switch (true)
        {
            case var condition when direction == Vector2.up:
                spriteRenderUp.enabled = true;
                spriteRenderDown.enabled = false;
                spriteRenderLeft.enabled = false;
                spriteRenderRight.enabled = false;
                activeSpriteRender = spriteRenderUp;
                break;
            case var condition when direction == Vector2.down:
                spriteRenderUp.enabled = false;
                spriteRenderDown.enabled = true;
                spriteRenderLeft.enabled = false;
                spriteRenderRight.enabled = false;
                activeSpriteRender = spriteRenderDown;
                break;
            case var condition when direction == Vector2.left:
                spriteRenderUp.enabled = false;
                spriteRenderDown.enabled = false;
                spriteRenderLeft.enabled = true;
                spriteRenderRight.enabled = false;
                activeSpriteRender = spriteRenderLeft;
                break;
            case var condition when direction == Vector2.right:
                spriteRenderUp.enabled = false;
                spriteRenderDown.enabled = false;
                spriteRenderLeft.enabled = false;
                spriteRenderRight.enabled = true;
                activeSpriteRender = spriteRenderRight;
                break;
        }

        activeSpriteRender.idle = direction == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (view.IsMine && !isDeath && collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            Camera camera = Camera.main;
            camera.GetComponent<AudioSource>().enabled = false;
            GetComponent<BombController>().enabled = false;
            audioSource.Play();
            view.RPC(nameof(OnDeath), RpcTarget.AllBuffered);
            //OnDeath(collision.gameObject);
        }
    }

    [PunRPC]
    void OnDeath()
    {

        spriteRenderUp.enabled = false;
        spriteRenderDown.enabled = false;
        spriteRenderLeft.enabled = false;
        spriteRenderRight.enabled = false;
        spriteRenderDeath.enabled = true;

        Invoke(nameof(OnDeathEnded), 1.25f);
    }

    void OnDeathEnded()
    {
        gameObject.SetActive(false);
    }
}
