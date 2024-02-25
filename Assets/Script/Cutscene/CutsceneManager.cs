using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;

    [SerializeField] private List<Cutscene> cutscenes;
    [SerializeField] private int loadScene = 2;
    [SerializeField] private GameObject loadingScreen;
    private int indexCutscene;

    private void Awake()
    {
        Instance = this;
        StartCutscene();
    }

    public void StartCutscene()
    {
        indexCutscene = 0;
        cutscenes[indexCutscene].Show();
        indexCutscene++;
    }

    public void NextCutscene()
    {
        if (cutscenes.ElementAtOrDefault(indexCutscene) == null)
        {
            // End
            SceneManager.LoadScene(loadScene);
            loadingScreen?.SetActive(true);
            return;
        }

        cutscenes[indexCutscene].Show();
        indexCutscene++;
    }
}
