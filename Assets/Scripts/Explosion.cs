using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AnimationSpriteRender startSprite;
    public AnimationSpriteRender middleSprite;
    public AnimationSpriteRender endSprite;

    public void SetActiveSprite(AnimationSpriteRender sprite)
    {
        startSprite.enabled = sprite == startSprite;
        middleSprite.enabled = sprite == middleSprite;
        endSprite.enabled = sprite == endSprite;
    }

    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void DestroyAfter(float time)
    {
        Destroy(gameObject, time);
    }
}
