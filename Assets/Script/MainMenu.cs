using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public Dropdown qualityDropdown;

    private void Start(){
        PopulateQualityDropdown();
    }

    public void LoadLevel(){
        SceneManager.LoadScene("HUB");
    }

    public void Exit(){
            Application.Quit();
    }

    public void PopulateQualityDropdown(){
        string[] qualityLevels = QualitySettings.names;
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new List<string>(qualityLevels));
        int savedQualityLevel = PlayerPrefs.GetInt("QualityLevel", QualitySettings.GetQualityLevel());
        SetDropdownValue(savedQualityLevel);
        SetQualityLevel(savedQualityLevel);
    }

    public void SetQualityLevel(int value){
        QualitySettings.SetQualityLevel(value);
        PlayerPrefs.SetInt("QualityLevel", value);
        PlayerPrefs.Save();
    }

    private void SetDropdownValue(int value){
        qualityDropdown.SetValueWithoutNotify(value);
    }
}