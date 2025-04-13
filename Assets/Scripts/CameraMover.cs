using UnityEngine;

public class CameraMover : MonoBehaviour
{[Header("Настройки")]
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.1f; 
    [SerializeField] private float xOffset = 0f; 
    [SerializeField] private float minX = -10f;  
    [SerializeField] private float maxX = 10f; 

    private float initialY;

    private void Start()
    {     
        initialY = transform.position.y;
    }

    private void LateUpdate()
    {
        float targetX = target.position.x + xOffset;
        float clampedX = Mathf.Clamp(targetX, minX, maxX);

        Vector3 newPosition = new Vector3(
            Mathf.Lerp(transform.position.x, clampedX, smoothSpeed),
            initialY, 
            transform.position.z
        );

        transform.position = newPosition;
    }
}
