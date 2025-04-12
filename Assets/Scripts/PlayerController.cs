using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float healAmount =15;
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

        // Определяем направление атаки
        Vector2 attackDirection = movementDirection.normalized; // Направление движения игрока

        if (attackDirection == Vector2.zero)
        {
            // Если игрок стоит на месте, используем направление взгляда по умолчанию (вправо)
            attackDirection = Vector2.right;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, attackDirection, 3f);

        if (hit.collider != null)
        {
            Debug.Log($"Raycast попал в объект: {hit.collider.name}");

            if (hit.collider.CompareTag("Enemy")) // Если это враг
            {
                EnemyAI enemy = hit.collider.GetComponent<EnemyAI>();
                if (enemy != null)
                {
                    Debug.Log("Наносим урон демону лени.");
                    enemy.TakeDamage(10f); // Наносим урон демону лени
                }
            }
            else if (hit.collider.CompareTag("Goblin")) // Если это гоблин
            {
                GoblinAI goblin = hit.collider.GetComponent<GoblinAI>();
                if (goblin != null)
                {
                    Debug.Log("Наносим урон гоблину.");
                    goblin.TakeDamage(10f); // Наносим урон гоблину
                    playerStats.ModifyStat("Anger", -10f); // Уменьшаем гнев
                }
            }
        }
        else
        {
            Debug.Log("Raycast не попал ни в один объект.");
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
            playerStats.Heal(healAmount);
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(movementDirection.normalized * 3f));
    }
}