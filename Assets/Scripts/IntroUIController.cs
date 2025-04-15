using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroUIController : MonoBehaviour
{
    [SerializeField] private Image skull;
    [SerializeField] private GameObject buttons;

    [SerializeField] private TMP_Text skullText;
    [SerializeField] private TMP_Text godText;

    private string[] _dialogs;

    private string _inputLine;

    private int _num = 0;
    private int _lastText;

    private string _currString;

    [SerializeField] private GameObject finalPanel;

    [SerializeField] private Image shade;
    
    string introPath = System.IO.Path.Combine(Application.streamingAssetsPath, "INTROtext.txt");
    string outroPathT = System.IO.Path.Combine(Application.streamingAssetsPath, "OUTROtextTRUE.txt");
    string outroPathF = System.IO.Path.Combine(Application.streamingAssetsPath, "OUTROtextFALSE.txt");

    [SerializeField] private AudioSource mainSource;
    [SerializeField] private AudioClip godClip;
    [SerializeField] private AudioClip skullClip;

    

    private void Awake()
    {
        float win = PlayerPrefs.GetFloat("Win");
        
        StartCoroutine(Skull());
        
        if (SceneManager.GetActiveScene().buildIndex==1) ReadFile(introPath);
        else
        {
            if (win==0f) ReadFile(outroPathT);
            else ReadFile(outroPathF);
        }
    }
    
 
    public void NextIntro()
    {
        StopAllCoroutines();
        _num++;
        if (_num != _lastText)
        {
            TextChange(_num);
        }
        else StartCoroutine(Shading());

    }

    void TextChange(int num)
    {
        skullText.text="";
        godText.text="";

        _currString = _dialogs[num];
        mainSource.Stop();

        if (_currString[0] == '~')
        {
            _currString=_currString.Replace("~", "");
            StartCoroutine(TypeText(_currString, godText));
            mainSource.clip = godClip;
        }

        else
        {
            StartCoroutine(TypeText(_currString, skullText));
            mainSource.clip = skullClip;
            
        }
        mainSource.Play();
    }
    
    
    IEnumerator TypeText(string fullText, TMP_Text text)
    {
        foreach (char c in fullText)
        {
            text.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        mainSource.Stop();
    }
    
    
    public void Skip()
    {
        StartCoroutine(Shading());
    }

    void ReadFile(string path)
    {
        _dialogs = System.IO.File.ReadAllLines(path);
        _lastText = _dialogs.Length;
    }

    IEnumerator Skull()
    {
        yield return new WaitForSeconds(1.5f);
        float a=1f;
        while (a > 0f)
        {
            a -= 0.02f;
            skull.color = new Color(1f, 1f, 1f, a);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1.5f);
        skull.gameObject.SetActive(false);
        buttons.SetActive(true);
        TextChange(0);
    }

    public void SkipOutro()
    {
        finalPanel.SetActive(true);
    }
    
    public void Exit()
    {
        Application.Quit();
    }
    
    IEnumerator Shading()
    {
        shade.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        float a=0;
        while (a < 1f)
        {
            a += 0.03f;
            shade.color = new Color(0, 0, 0, a);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(2);
    }
}


