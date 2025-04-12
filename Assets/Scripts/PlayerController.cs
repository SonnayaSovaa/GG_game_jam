using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    //public float healAmount =15;
    private InputActions inputActions;
    private Vector2 movementDirection;

    public PlayerStats playerStats;

    // Флаг для взаимодействия
    private bool interactPressed = false;
    private GameObject currentEnemy;

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
        if (currentEnemy != null)
        {
            Debug.Log($"Атакуем врага: {currentEnemy.name}");

            if (currentEnemy.CompareTag("Enemy")) // Если это демон лени
            {
                EnemyAI enemy = currentEnemy.GetComponent<EnemyAI>();
                if (enemy != null)
                {
                    enemy.TakeDamage(10f); // Наносим урон демону лени
                }
            }
            else if (currentEnemy.CompareTag("Goblin")) // Если это гоблин
            {
                GoblinAI goblin = currentEnemy.GetComponent<GoblinAI>();
                if (goblin != null)
                {
                    goblin.TakeDamage(10f); // Наносим урон гоблину
                    playerStats.ModifyStat("Anger", -10f); // Уменьшаем гнев
                }
            }
        }
        else
        {
            Debug.Log("Перед игроком нет врагов.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Goblin"))
        {
            Debug.Log($"Враг обнаружен: {other.name}");
            currentEnemy = other.gameObject; // Сохраняем ссылку на текущего врага
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Goblin"))
        {
            Debug.Log($"Враг покинул зону видимости: {other.name}");
            if (currentEnemy == other.gameObject)
            {
                currentEnemy = null; // Очищаем ссылку на врага
            }
        }
    }

    /*private void OnTriggerStay2D(Collider2D other)
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
    }*/

   /* private void CollectCoin(GameObject coin)
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
        }

        Destroy(coin);
    }*/

    /*private void UseHealthItem(GameObject healthItem)
    {
        Debug.Log("Использован предмет здоровья!");

        bool pickUp = IsPickUp(); // Логика для флага

        if (pickUp)
        {
            playerStats.Heal(healAmount);
            Debug.Log("Вы съели предмет здоровья.");
            playerStats.ModifyStat("Gluttony", -10f); 
        }
       /* else
        {
            playerStats.ModifyStat("Gluttony", 10f); 
        }*

        Destroy(healthItem);
    }*/

   /* private bool IsPickUp()
    {
        return interactPressed; // Используем флаг взаимодействия
    }*/
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(movementDirection.normalized * 3f));
    }
}