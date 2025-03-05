using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManagerBasement : MonoBehaviour
{
    public static GameManagerBasement Instance;

    [SerializeField] private EventOnChangeState[] eventsOnChangeState;
    [SerializeField] GameObject deadCutscene;
    [SerializeField] GameObject character;

    public State StateOfGame { private set; get; }



    private void Awake()
    {
        Instance = this;

        StateOfGame = new State();
        ChangeStateOfGame(State.CutsceneNumber01);
    }

    public void ChangeStateOfGame(State state)
    {
        if (state == StateOfGame)
            return;

        foreach(var item in eventsOnChangeState)
        {       
            if(StateOfGame != State.None)
            {
                if (item.StateID == StateOfGame)
                {
                    item.onExitState.Invoke();
                    continue;
                }
            }

            if(item.StateID == state)
            {
                item.onEnterState.Invoke();
            }
        }

        StateOfGame = state;

        if(StateOfGame == State.Gameover) 
        { 
            deadCutscene.SetActive(true);
            character.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ChangeStateOfGameByInt(int state)
    {
        ChangeStateOfGame((State)state);
    }

    public enum State
    {
        None,
        CutsceneNumber01,
        ChairSystem,
        SolveShadowPuzzle,
        FightWithBot,
        Gameover
    }

    [Serializable]
    private struct EventOnChangeState
    {
        public State StateID;

        public UnityEvent onEnterState;
        public UnityEvent onExitState;
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
