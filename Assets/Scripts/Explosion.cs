using Photon.Pun;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AnimationSpriteRender startSprite;
    public AnimationSpriteRender middleSprite;
    public AnimationSpriteRender endSprite;
    public float timeDuration = 1f;

    [SerializeField] private AudioSource audioSource;

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

    [PunRPC]
    void SetExplosion(string animationSprite, Vector2? direction)
    {
        switch (animationSprite)
        {
            case "start":
                SetActiveSprite(startSprite);
                break;
            case "middle":
                SetActiveSprite(middleSprite);
                audioSource.Play();
                break;
            case "end":
                SetActiveSprite(endSprite);
                break;
        }
        SetDirection(direction ?? Vector2.up);
        Destroy(this.gameObject, timeDuration);
    }
}
