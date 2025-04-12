using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
   [SerializeField] Slider slider;
   [SerializeField] AudioMixer mixer;
    void Start()
    {
        mixer.SetFloat("Master", Mathf.Log10(slider.value) * 20);
    }

    public void VolumeChange()
   {
        mixer.SetFloat("Master", Mathf.Log10(slider.value) * 20);
   }   
}
