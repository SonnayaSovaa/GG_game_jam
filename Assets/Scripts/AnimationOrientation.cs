using UnityEngine;

public class AnimationOrientation : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rg;
    [SerializeField] private SpriteRenderer sprite;
    private bool isFacingRignt;   
   

    void Update()
    {
        Vector2 direction = rg.linearVelocity.normalized;
        if (direction.x != 0)
        {
            Debug.Log('l');
            if (direction.x < 0)
            {
                isFacingRignt = false;
            }
            else
            {
                isFacingRignt = true;
            }
            sprite.flipX = !isFacingRignt;
        }
    }
}
