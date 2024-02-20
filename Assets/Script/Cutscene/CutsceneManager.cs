using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;

    [SerializeField] private List<Cutscene> cutscenes;
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
            return;
        }

        cutscenes[indexCutscene].Show();
        indexCutscene++;
    }
}
