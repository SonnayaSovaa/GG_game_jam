using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health = 100f;
    public float anger = 0f; // Гнев
    public float greed = 0f; // Алчность
    public float gluttony = 0f; // Чревоугодие
    public float laziness = 0f; // Лень

    public void ModifyStat(string statName, float value)
    {
        switch (statName)
        {
            case "Anger":
                anger += value;
                Debug.Log($"Гнев: {anger}");
                break;
            case "Greed":
                greed += value;
                Debug.Log($"Алчность: {greed}");
                break;
            case "Gluttony":
                gluttony += value;
                Debug.Log($"Чревоугодие: {gluttony}");
                break;
            case "Laziness":
                laziness += value;
                Debug.Log($"Лень: {laziness}");
                break;
        }
    }

    // Получение текущего значения характеристики
    public float GetStat(string statName)
    {
        switch (statName)
        {
            case "Anger": return anger;
            case "Greed": return greed;
            case "Gluttony": return gluttony;
            case "Laziness": return laziness;
            default: return 0f;
        }
    }


    public void TakeDamage(float amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0f, maxHealth);
        if (health <= 0)
        {
            Debug.Log("Игрок умер!");
            RestartLevel();
        }
    }
    public void Heal(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0f, maxHealth); 

        Debug.Log($"Игрок восстановил здоровье! Текущее здоровье: {health}");
    }

        private void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}