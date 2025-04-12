using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonsController : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject controlPanel;


    public void Exit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void ToControls()
    {
        mainPanel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void ToMain()
    {
        controlPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

}
