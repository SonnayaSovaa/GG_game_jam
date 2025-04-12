using UnityEngine;

public class ItemCollider : MonoBehaviour
{
    [SerializeField] private GameObject itemCanvas;
    [SerializeField] public float statChangeValue;
    [SerializeField] public float healAmount;
    private void OnTriggerEnter2D(Collider2D other)
    {
        {
            itemCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            itemCanvas.SetActive(false); 
        }
    }
}
