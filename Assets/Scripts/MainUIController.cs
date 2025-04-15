using System;
using UnityEngine;

public class MainUIController : MonoBehaviour
{

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject statsPanel;

    private bool _onPause=false;
    private bool _onStats=false;


    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.PlayerINPT.Pause.started += ctx => PauseCheck();
    }
    private void OnEnable()
    {
        inputActions.PlayerINPT.Enable();
    }

    private void OnDisable()
    {
        inputActions.PlayerINPT.Disable();
    }
    void PauseCheck()
    {
        if (_onPause)
        {
            if (_onStats) StatsDeactivate();
            else Continue();
        }
        else OnPause();
        
        
    }
    
    public void OnPause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        _onPause = true;
        _onStats = false;
    }

    public void Continue()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        _onStats = false;
        _onPause = false;
    }
    
    public void Exit()
    {
        Application.Quit();
        _onPause = false;
    }

    public void StatsActivate()
    {
        Time.timeScale = 0;
        statsPanel.SetActive(true);
        _onPause = true;
        _onStats = true;
    }
    
    public void StatsDeactivate()
    {
        Time.timeScale = 1;
        statsPanel.SetActive(false);
        _onPause = false;
        _onStats = false;
    }
}
