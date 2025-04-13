using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    public float health = 100f;
    // Скорость замедления игрока
    public float slowAmount = 0.5f;
    public float attackDamag = 5f;

    public float lazinessInterval = 1f;
    private float lazinessTimer = 0f;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        
        agent.speed = 1f;
    }

    private void Update()
    {
        if (target != null && agent.isOnNavMesh)
        {
            agent.SetDestination(target.position);
        }

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Демон лени замедляет игрока!");
            PlayerController playerController = other.GetComponent<PlayerController>();
            PlayerStats playerStats = other.GetComponent<PlayerStats>();

            if (playerController != null)
            {
                playerController.moveSpeed *= slowAmount; // Замедляем игрока
            }

            if (playerStats != null)
            {
                playerStats.ModifyStat("Laziness", -10f); // Увеличиваем лень
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lazinessTimer += Time.deltaTime;

            // Проверяем, прошло ли достаточно времени для накопления лени
            if (lazinessTimer >= lazinessInterval)
            {
                lazinessTimer = 0f; // Сбрасываем таймер

                PlayerStats playerStats = other.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.TakeDamage(attackDamag);
                    playerStats.ModifyStat("Laziness", -10f); // Уменьшаем лень (делаем её более отрицательной)
                    Debug.Log("Ленивость накапливается...");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.moveSpeed /= slowAmount; // Восстанавливаем скорость
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Демон лени получил урон! Осталось здоровья: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Демон лени уничтожен!");
        Destroy(gameObject);
    }
}