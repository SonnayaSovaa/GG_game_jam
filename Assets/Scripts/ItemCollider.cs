using UnityEngine;
using UnityEngine.InputSystem;

public class ItemCollider : MonoBehaviour
{
    [SerializeField] private GameObject itemCanvas;
    [SerializeField] public float statChangeValue;
    [SerializeField] public float healAmount = 20f;

    private bool isInteractable = false;
    private InputActions inputActions;

    private void Awake()
    {

        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.PlayerINPT.Interact.Enable();
    }

    private void OnDisable()
    {
        inputActions.PlayerINPT.Interact.Disable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractable = false;
            itemCanvas.SetActive(false);
        }
    }

    private void Update()
    {

        if (isInteractable && inputActions.PlayerINPT.Interact.triggered)
        {
            itemCanvas.SetActive(true);
        }
    }
}