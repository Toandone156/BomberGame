using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 2f;
    public Transform target;
    public float minX = -6.6f;
    public float minY = -1.5f;
    public float maxX = 6.6f;
    public float maxY = 1.5f;

    void Update()
    {
        if (target != null)
        {
            var moveX = target.position.x;
            var moveY = target.position.y;

            var x = moveX > maxX ? maxX : (moveX < minX ? minX : moveX);
            var y = moveY > maxY ? maxY : (moveY < minY ? minY : moveY);
            Vector3 newPos = new Vector3(x, y, -10f);
            transform.position = Vector3.Lerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }

    }

    public void setPlayer(GameObject player)
    {
        target = player.transform;
    }

}
