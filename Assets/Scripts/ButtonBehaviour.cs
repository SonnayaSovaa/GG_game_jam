using UnityEngine;
using UnityEngine.Serialization;

public class ButtonBehaviour : MonoBehaviour
{
    private Vector3 _startScale;
    private float _scaleFactor = 1.07f;
    [SerializeField] private AudioClip onHoverSound;
    [SerializeField] private AudioSource audioSource;

    public void OnHover(Transform obj)
    {
        _startScale = obj.localScale;
        obj.localScale *= _scaleFactor;
        //audioSource.PlayOneShot(onHoverSound);
    }

    public void ExitHover(Transform obj)
    {
        obj.localScale = _startScale;
    }
}
