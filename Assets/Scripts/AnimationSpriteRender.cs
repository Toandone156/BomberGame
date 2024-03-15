using UnityEngine;

public class AnimationSpriteRender : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public float animationTime = 0.25f;
    private int animationFrame;

    public Sprite idleSprite;
    public Sprite[] sprites;

    public bool loop = true;
    public bool idle = true;
    [SerializeField] private AudioSource audioSource;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    void Start()
    {
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
    }

    private void NextFrame()
    {
        animationFrame++;

        if (loop) animationFrame %= sprites.Length;
        else if (animationFrame >= sprites.Length) return;

        if (idle)
        {
            spriteRenderer.sprite = idleSprite;
        }
        else
        {
            spriteRenderer.sprite = sprites[animationFrame];
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioSource.Play();
        }
    }
}
