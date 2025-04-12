using UnityEngine;
using UnityEngine.InputSystem;

public class ItemCollider : MonoBehaviour
{
    [SerializeField] private GameObject itemCanvas; // UI-меню взаимодействия
    [SerializeField] public float statChangeValue; // Значение изменения характеристик
    [SerializeField] public float healAmount; // Значение восстановления здоровья

    private bool isInteractable = false; // Флаг: можно ли взаимодействовать
    private InputActions inputActions; // Ссылка на InputActions

    private void Awake()
    {
        // Инициализируем InputActions
        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.PlayerINPT.Interact.Enable(); // Включаем действие "Interact"
    }

    private void OnDisable()
    {
        inputActions.PlayerINPT.Interact.Disable(); // Отключаем действие "Interact"
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Игрок вошёл в зону взаимодействия.");
            isInteractable = true; // Разрешаем взаимодействие
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Игрок покинул зону взаимодействия.");
            isInteractable = false; // Запрещаем взаимодействие
            itemCanvas.SetActive(false); // Скрываем меню
        }
    }

    private void Update()
    {
        // Проверяем, находится ли игрок в зоне взаимодействия и нажал ли он клавишу взаимодействия
        if (isInteractable && inputActions.PlayerINPT.Interact.triggered)
        {
            Debug.Log("Игрок нажал кнопку взаимодействия.");
            itemCanvas.SetActive(true); // Показываем меню взаимодействия
        }
    }
}