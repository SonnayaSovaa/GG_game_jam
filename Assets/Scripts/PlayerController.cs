using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    private InputActions inputActions;
    private Vector2 movementDirection;

    public PlayerStats playerStats;

    // Флаг для взаимодействия
    private bool interactPressed = false;

    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.PlayerINPT.MoveUp.performed += ctx => movementDirection.y = 1;
        inputActions.PlayerINPT.MoveUp.canceled += ctx => movementDirection.y = 0;

        inputActions.PlayerINPT.MoveDown.performed += ctx => movementDirection.y = -1;
        inputActions.PlayerINPT.MoveDown.canceled += ctx => movementDirection.y = 0;

        inputActions.PlayerINPT.MoveLeft.performed += ctx => movementDirection.x = -1;
        inputActions.PlayerINPT.MoveLeft.canceled += ctx => movementDirection.x = 0;

        inputActions.PlayerINPT.MoveRight.performed += ctx => movementDirection.x = 1;
        inputActions.PlayerINPT.MoveRight.canceled += ctx => movementDirection.x = 0;

        inputActions.PlayerINPT.Attack.performed += ctx => Attack();
        inputActions.PlayerINPT.Interact.performed += ctx => interactPressed = true;
        inputActions.PlayerINPT.Interact.canceled += ctx => interactPressed = false;

        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void OnEnable()
    {
        inputActions.PlayerINPT.Enable();
    }

    private void OnDisable()
    {
        inputActions.PlayerINPT.Disable();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        rb.linearVelocity = movementDirection * moveSpeed;
    }

    private void Attack()
    {
        Debug.Log("Атака!");

        // Проверяем, есть ли объект перед игроком
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1f);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy")) 
            {
                playerStats.ModifyStat("Laziness", 10f);
            }
            else if (hit.collider.CompareTag("Goblin")) // Если это гоблин
            {
                Debug.Log("Убивать гоблинов нельзя!");
                playerStats.ModifyStat("Anger", -10f); // Увеличиваем гнев
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (interactPressed)
        {
            if (other.CompareTag("Coin"))
            {
                CollectCoin(other.gameObject);
            }
            else if (other.CompareTag("HealthItem"))
            {
                UseHealthItem(other.gameObject);
            }
        }
    }

    private void CollectCoin(GameObject coin)
    {
        Debug.Log("Взаимодействие с монетой!");

        bool pickUp = IsPickUp(); 

        if (pickUp)
        {
            Debug.Log("Вы подобрали монету.");
            playerStats.ModifyStat("Greed", -10f); // Уменьшаем алчность
        }
        /*else
        /*else
        {
            playerStats.ModifyStat("Greed", 10f); // Увеличиваем алчность
        }*/

        Destroy(coin);
    }

    private void UseHealthItem(GameObject healthItem)
    {
        Debug.Log("Использован предмет здоровья!");

        bool pickUp = IsPickUp(); // Логика для флага

        if (pickUp)
        {
            Debug.Log("Вы съели предмет здоровья.");
            playerStats.ModifyStat("Gluttony", -10f); 
        }
       /* else
        {
            playerStats.ModifyStat("Gluttony", 10f); 
        }*/

        Destroy(healthItem);
    }

    private bool IsPickUp()
    {
        return interactPressed; // Используем флаг взаимодействия
    }
}