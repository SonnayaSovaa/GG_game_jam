using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using Unity.VisualScripting;
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

    [SerializeField] private AudioSource godSource;
    [SerializeField] private AudioSource skullSource;

    private string _currString;

    [SerializeField] private GameObject finalPanel;

    [SerializeField] private Image shade;
    
    string introPath = System.IO.Path.Combine(Application.streamingAssetsPath, "INTROtext.txt");
    string outroPathT = System.IO.Path.Combine(Application.streamingAssetsPath, "OUTROtextTRUE.txt");
    string outroPathF = System.IO.Path.Combine(Application.streamingAssetsPath, "OUTROtextFALSE.txt");

    

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

        if (_currString[0] == '~')
        {
            _currString=_currString.Replace("~", "");
            StartCoroutine(TypeText(_currString, godText, godSource));
            godSource.Play();
        }

        else
        {
            StartCoroutine(TypeText(_currString, skullText, skullSource));
            skullSource.Play();
        }
    }
    
    
    IEnumerator TypeText(string fullText, TMP_Text text, AudioSource source)
    {
        foreach (char c in fullText)
        {
            text.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        source.Stop();
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
    
    /*
    
    void ReadTextFile(string file_path)
    {
        StreamReader _stream = new StreamReader(file_path);
        
        while(!_stream.EndOfStream)
        {
            _inputLine = _stream.ReadLine( );
            _dialogs.Add(_inputLine);
        }

        _stream.Close( );  
        _lastText = _dialogs.Count;

        _dialogs = System.IO.File.ReadAllLines(_SAfilePath);
    }
    
*/
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


