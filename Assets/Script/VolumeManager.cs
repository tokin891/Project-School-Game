using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField]
    private Slider volumeSlider;
    public static VolumeManager instance;

    private void Start() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            float savedVolume = PlayerPrefs.GetFloat("Volume", AudioListener.volume);
            SetVolume(savedVolume);
            SetSliderValue(savedVolume);
        }
        else {
            Destroy(gameObject);
            return;
        }
    }

    public void SetVolume(float volume) {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public void SetSliderValue(float value) {
        volumeSlider.value = value;
    }
}
