using UnityEngine;
using UnityEngine.AI;

public class GoblinAI : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 3f; // Быстрая скорость
    public float attackDamage = 10f; // Урон от атаки
    private NavMeshAgent agent;

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
}