using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
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
}
