using System;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public class ItemCanvasController : MonoBehaviour
{
    //[SerializeField] private bool food = false;
    [SerializeField] private TMP_Text applyText;
    [SerializeField] private GameObject mainObj;

    private PlayerStats playerStats;
    private ItemCollider itemCollider;

    [SerializeField] private TMP_Text coins;
    private void Awake()
    {
        playerStats = Object.FindAnyObjectByType<PlayerStats>();
        itemCollider = mainObj.GetComponent<ItemCollider>();
     
        if (mainObj.CompareTag("Coin"))
        {
            applyText.text = "подобрать"; // Если это монета, текст кнопки "Подобрать"
        }
        else if (mainObj.CompareTag("HealthItem"))
        {
            applyText.text = "съесть"; // Если это еда, текст кнопки "съесть"
        }
    }

    public void Apply()
    {
        Debug.Log("Игрок выбрал действие 'Применить'");

        if (playerStats == null)
        {
            Debug.LogError("PlayerStats не найден!");
            return;
        }

        if (mainObj.CompareTag("Coin"))
        {
            // Если это монета
            playerStats.ModifyStat("Greed", -itemCollider.statChangeValue); // Уменьшаем алчность
            Debug.Log("Игрок подобрал монету.");
            coins.text = Convert.ToString(Convert.ToInt32(coins.text) + 10);
        }
        else if (mainObj.CompareTag("HealthItem"))
        {
            // Если это еда (HealthItem)
            playerStats.ModifyStat("Gluttony", -itemCollider.statChangeValue); // Уменьшаем чревоугодие
            playerStats.Heal(itemCollider.healAmount); // Восстанавливаем здоровье
            Debug.Log("Игрок съел предмет здоровья.");
        }

        Destroy(mainObj);
    }

    public void Discard()
    {
        Debug.Log("Игрок выбрал действие 'Бросить'");

        if (playerStats == null)
        {
            Debug.LogError("PlayerStats не найден!");
            return;
        }

        if (mainObj.CompareTag("Coin"))
        {
            // Если это монета
            playerStats.ModifyStat("Greed", itemCollider.statChangeValue); 
            Debug.Log("Игрок проигнорировал монету.");
        }
        else if (mainObj.CompareTag("HealthItem"))
        {
            // Если это еда (HealthItem)
            playerStats.ModifyStat("Gluttony", itemCollider.statChangeValue); 
            Debug.Log("Игрок бросил предмет здоровья.");
        }

        Destroy(mainObj);
    }
}
