using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(Slider))]
public class VolumeSetter : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string field;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        float value;
        audioMixer.GetFloat(field, out value);

        slider.value = Mathf.Pow(10, ((value) / 20));
    }

    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat(field, Mathf.Log10(sliderValue) * 20);
    }
}