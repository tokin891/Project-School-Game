using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityManager : MonoBehaviour
{
    [SerializeField]
    private Slider sensitivitySlider;

    public static SensitivityManager instance;

    private void Start(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
            float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", 1.0f);
            SetSensitivity(savedSensitivity);
            SetSliderValue(savedSensitivity);

        } else{
            Destroy(gameObject);
            return;
        }
    }

    public void SetSensitivity(float sensitivity){
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
        PlayerPrefs.Save();
    }

    public void SetSliderValue(float value){
        sensitivitySlider.value = value;
    }
}
