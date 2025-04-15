using UnityEngine;
using UnityEngine.AI;

public class GoblinAI : MonoBehaviour
{
    public Animator anim;
    public Transform target;
    public float moveSpeed = 3f; // Быстрая скорость
    public float attackDamage = 10f; // Урон от атаки
    [SerializeField] private NavMeshAgent agent;
    public float health = 50f;

    [SerializeField] private SpriteRenderer sprite;
    private float _prevPointX;

    public AudioSource audio;
    public float attackInterval = 0.2f;
    private float timer = 0f;

    private void Start()
    {
        //agent = GetComponent<NavMeshAgent>();

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        _prevPointX = transform.position.x;
        agent.speed = moveSpeed;
    }

    private void Update()
    {
        timer += Time.deltaTime; // Накапливаем время
        if (target != null && agent.isOnNavMesh)
        {
            agent.SetDestination(target.position);
            anim.SetBool("walk", true);

            if (_prevPointX < transform.position.x) sprite.flipX = false;
            else sprite.flipX = true;
            
            _prevPointX = transform.position.x;

        }
        else anim.SetBool("walk", false);
        
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.Play("goblin_attack");
            Debug.Log("Гоблин атакует игрока!");
            PlayerStats playerStats = other.GetComponent<PlayerStats>();

            if (playerStats != null)
            {
                playerStats.TakeDamage(attackDamage); // Наносим урон игроку
            }
            //anim.SetBool("attack", false);
        }
    }*/

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {          

            if (timer >= attackInterval)
            {
                anim.Play("goblin_attack");
                Debug.Log("Гоблин атакует игрока!");
                PlayerStats playerStats = other.GetComponent<PlayerStats>();

                if (playerStats != null)
                {
                    playerStats.TakeDamage(attackDamage); 
                    audio.Play();// Наносим урон игроку
                }
                timer = 0f; 
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