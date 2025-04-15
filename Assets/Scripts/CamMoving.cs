using UnityEngine;

public class CamMoving : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.1f; 
    [SerializeField] private float xOffset = 0.05f;

    [SerializeField] private float minY = -5.471545f;

    private void LateUpdate()
    {
        float targetX = target.position.x + xOffset;
        float clampedY = Mathf.Clamp(target.position.y, minY, 100);

        Vector3 newPosition = new Vector3(
            Mathf.Lerp(transform.position.x, targetX, smoothSpeed),
            clampedY,
            transform.position.z
        );

        transform.position = newPosition;
    }
}
