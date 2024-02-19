using System.Collections;
using System.Collections.Generic;
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

    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRender = spriteRenderDown;
    }

    public void Update()
    {
        switch (true)
        {
            case var c when Input.GetKey(up):
                SetDirection(Vector2.up, spriteRenderUp);
                break;
            case var c when Input.GetKey(down):
                SetDirection(Vector2.down, spriteRenderDown);
                break;
            case var c when Input.GetKey(left):
                SetDirection(Vector2.left, spriteRenderLeft);
                break;
            case var c when Input.GetKey(right):
                SetDirection(Vector2.right, spriteRenderRight);
                break;
            default:
                SetDirection(Vector2.zero, activeSpriteRender);
                break;
        }
    }

    public void FixedUpdate()
    {
        var position = rigidbody.position;
        var translation = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }

    private void SetDirection(Vector2 newDirection, AnimationSpriteRender spriteRender)
    {
        direction = newDirection;

        spriteRenderUp.enabled = spriteRender == spriteRenderUp;
        spriteRenderDown.enabled = spriteRender == spriteRenderDown;
        spriteRenderLeft.enabled = spriteRender == spriteRenderLeft;
        spriteRenderRight.enabled = spriteRender == spriteRenderRight;

        activeSpriteRender = spriteRender;

        activeSpriteRender.idle = direction == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            OnDeath(collision.gameObject);
        }
    }

    void OnDeath(GameObject player)
    {
        enabled = false;
        GetComponent<BombController>().enabled = false;

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
