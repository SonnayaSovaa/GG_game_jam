using UnityEngine;

public class ColliderOrientation : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        transform.position = target.position;
    }
}
