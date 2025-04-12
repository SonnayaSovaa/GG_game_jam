using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    // Скорость замедления игрока
    public float slowAmount = 0.5f;

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

    private void OnDestroy()
    {
        Debug.Log("Демон лени уничтожен!");
    }
}