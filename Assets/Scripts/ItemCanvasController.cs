using TMPro;
using UnityEngine;

public class ItemCanvasController : MonoBehaviour
{
    [SerializeField] private bool food = false;
    [SerializeField] private TMP_Text applyText;
    [SerializeField] private GameObject mainObj;

    private void Awake()
    {
        if (food) applyText.text = "съесть";
    }

    public void Apply()
    {
        //
    }

    public void Discard()
    {
        //
        
        Destroy(mainObj);
    }
}
