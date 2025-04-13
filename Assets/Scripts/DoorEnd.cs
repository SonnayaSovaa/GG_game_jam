using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorEnd : MonoBehaviour
{

    [SerializeField] private Image shade;
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        var player = other.GetComponent<PlayerStats>();
        float win = player.anger+player.gluttony+player.laziness+player.greed;
        PlayerPrefs.SetFloat("Win", win);
        StartCoroutine(Shading());
    }
    
   
    IEnumerator Shading()
    {
        shade.gameObject.SetActive(true);
        float a=0;
        while (a < 1f)
        {
            a += 0.03f;
            shade.color = new Color(0, 0, 0, a);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(3);

    }
}
