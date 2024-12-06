using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManagerBasement : MonoBehaviour
{
    public static GameManagerBasement Instance;

    [SerializeField] private EventOnChangeState[] eventsOnChangeState;

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
    }

    public void ChangeStateOfGameByInt(int state)
    {
        ChangeStateOfGame((State)state);
    }

    public enum State
    {
        None,
        CutsceneNumber01,
        Lvl1
    }

    [Serializable]
    private struct EventOnChangeState
    {
        public State StateID;

        public UnityEvent onEnterState;
        public UnityEvent onExitState;
    }
}
