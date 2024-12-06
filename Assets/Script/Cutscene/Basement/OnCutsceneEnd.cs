using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class OnCutsceneEnd : MonoBehaviour
{
    private bool isPlayVideo = false;
    private PlayableDirector pd;

    [SerializeField] private UnityEvent onCutsceneEnd;

    private void Awake()
    {
        pd = GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if(!isPlayVideo && pd.state == PlayState.Playing)
        {
            isPlayVideo = true;
        }

        if (isPlayVideo && pd.state != PlayState.Playing)
        {
            onCutsceneEnd.Invoke();
            Destroy(gameObject);
        }
    }
}
