using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonsController : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject controlPanel;

    [SerializeField] private Image shade;


    public void Exit()
    {
        Application.Quit();
    }

    public void Play()
    {
        StartCoroutine(Shading());
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
    
    IEnumerator Shading()
    {
        float a=0;
        while (a < 1f)
        {
            a += 0.03f;
            shade.color = new Color(0, 0, 0, a);
            yield return new WaitForSeconds(0.05f);
        }

        SceneManager.LoadScene(1);

    }

}
