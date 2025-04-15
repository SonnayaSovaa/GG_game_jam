using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health = 100f;
    public float anger; // Гнев
    public float greed; // Алчность
    public float gluttony; // Чревоугодие
    public float laziness; // Лень


    [SerializeField] private Slider angerBar;
    [SerializeField] private Slider greedBar;
    [SerializeField] private Slider gluttonyBar;
    [SerializeField] private Slider lazinessBar;

    [SerializeField] private TextMeshProUGUI angerText;
    [SerializeField] private TextMeshProUGUI greedText;
    [SerializeField] private TextMeshProUGUI gluttonyText;
    [SerializeField] private TextMeshProUGUI lazinessText;

    [SerializeField] private Slider healthBar;

    private void Update()
    {
        UpdateUI(); // Инициализируем UI
    }
    public void ModifyStat(string statName, float value)
    {
        switch (statName)
        {
            case "Anger":
                anger += value;
                anger = Mathf.Clamp(anger, angerBar.minValue, angerBar.maxValue); // Ограничиваем диапазон
                break;
            case "Greed":
                greed += value;
                greed = Mathf.Clamp(greed, greedBar.minValue, greedBar.maxValue); // Ограничиваем диапазон
                break;
            case "Gluttony":
                gluttony += value;
                gluttony = Mathf.Clamp(gluttony, gluttonyBar.minValue, gluttonyBar.maxValue); // Ограничиваем диапазон
                break;
            case "Laziness":
                laziness += value;
                laziness = Mathf.Clamp(laziness, lazinessBar.minValue, lazinessBar.maxValue); // Ограничиваем диапазон
                break;
        }
        UpdateUI();
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
        healthBar.value = health / maxHealth;
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

    private void UpdateUI()
    {
        // Обновляем прогресс-бары
        angerBar.value = Mathf.Clamp(anger, angerBar.minValue, angerBar.maxValue);
        greedBar.value = Mathf.Clamp(greed, greedBar.minValue, greedBar.maxValue);
        gluttonyBar.value = Mathf.Clamp(gluttony, gluttonyBar.minValue, gluttonyBar.maxValue);
        lazinessBar.value = Mathf.Clamp(laziness, lazinessBar.minValue, lazinessBar.maxValue);

        angerText.text = $"{anger}"; 
        greedText.text = $"{greed}"; 
        gluttonyText.text = $"{gluttony}";
        lazinessText.text = $"{laziness}";
    }
}