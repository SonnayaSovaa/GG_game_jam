using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroUIController : MonoBehaviour
{
    [SerializeField] private Image skull;
    [SerializeField] private GameObject buttons;

    [SerializeField] private TMP_Text skullText;
    [SerializeField] private TMP_Text godText;


    [SerializeField] private string path;

    public List<string> dialogs;

    private string _inputLine;

    private int _num = 0;
    private int _lastText;

    [SerializeField] private AudioSource godSource;
    [SerializeField] private AudioSource skullSource;

    private string _currString;

    private void Awake()
    {
        dialogs = new List<string>();
        StartCoroutine(Shading());
        ReadTextFile(path);
    }
    
    
    
    
    //----------------------------------------
    
    
    public void NextIntro()
    {
        StopAllCoroutines();
        _num++;
        if (_num != _lastText)
        {
            TextChange(_num);
        }
        else SceneManager.LoadScene(2);

    }

    void TextChange(int num)
    {
        skullText.text="";
        godText.text="";

        _currString = dialogs[num];

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

 
    
    //----------------------------------------
    
    public void Skip()
    {
        SceneManager.LoadScene(2);
    }
    
    void ReadTextFile(string file_path)
    {
        StreamReader _stream = new StreamReader(file_path);
        
        while(!_stream.EndOfStream)
        {
            _inputLine = _stream.ReadLine( );
            dialogs.Add(_inputLine);
        }

        _stream.Close( );  
        _lastText = dialogs.Count;
    }
    

    IEnumerator Shading()
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
}


