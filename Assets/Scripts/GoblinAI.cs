using UnityEngine;
using UnityEngine.AI;

public class GoblinAI : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 3f; // Быстрая скорость
    public float attackDamage = 10f; // Урон от атаки
    private NavMeshAgent agent;
    public float health = 50f;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player")?.transform;
        }


        agent.speed = moveSpeed;
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
            Debug.Log("Гоблин атакует игрока!");
            PlayerStats playerStats = other.GetComponent<PlayerStats>();

            if (playerStats != null)
            {
                playerStats.TakeDamage(attackDamage); // Наносим урон игроку
            }
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Гоблин получил урон! Осталось здоровья: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Гоблин уничтожен!");
        PlayerStats playerStats = target?.GetComponent<PlayerStats>();

        if (playerStats != null)
        {
            playerStats.ModifyStat("Anger", -10f); // Уменьшаем гнев (убийство гоблина)
        }

        Destroy(gameObject);
    }
}