using UnityEngine;

public class MainUIController : MonoBehaviour
{

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject statsPanel;
    
    public void OnPause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    
    public void Exit()
    {
        Application.Quit();
    }

    public void StatsActivate()
    {
        Time.timeScale = 0;
        statsPanel.SetActive(true);
    }
    
    public void StatsDeactivate()
    {
        Time.timeScale = 1;
        statsPanel.SetActive(false);
    }
}
