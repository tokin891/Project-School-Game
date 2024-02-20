using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(CanvasGroup))]
public class Cutscene : MonoBehaviour
{
    [SerializeField] ObjectDetails[] allObjects;
    [SerializeField] float durationShow = 1f;
    [SerializeField] float durationObjectDetails = 4f;
    [SerializeField] float showNextCutscene = 2f;
 
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    public void Show()
    {
        if(canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
        }

        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, durationShow);
        StartCoroutine(WaitForLaunch());
        StartCutscene();
    }

    private IEnumerator WaitForLaunch()
    {
        while (canvasGroup.alpha != 1f)
        {
            yield return null;
        }

        StartCoroutine(WaitForSkip());
    }

    private IEnumerator WaitForSkip()
    {
        float timeToEnd = Time.time + (durationObjectDetails - durationShow);
        while (timeToEnd > Time.time)
        {
            if(Input.GetKeyDown(KeyCode.Space))
                timeToEnd = Time.time;
            yield return null;
        }

        EndCutscene();
    }

    private IEnumerator WaitForShutdown()
    {
        while(canvasGroup.alpha != 0f)
        {
            yield return null;
        }
        float timeToNextCutscene = Time.time + showNextCutscene;
        foreach (var obj in allObjects)
        {
            if (obj.PlaySound)
            {
                if (obj.MainObject.TryGetComponent(out AudioSource audioSource))
                {
                    audioSource.Stop();
                }
            }
        }
        while (Time.time < timeToNextCutscene)
        {
            yield return null;
        }    

        CutsceneManager.Instance.NextCutscene();
    }

    private void StartCutscene()
    {
        for (int i = 0; i < allObjects.Length; i++)
        {
            if (allObjects[i].Scale)
            {
                allObjects[i].MainObject.transform.DOScale(Vector3.one * allObjects[i].Proportion, durationObjectDetails);
            }

            if (allObjects[i].PlaySound)
            {
                if (allObjects[i].MainObject.TryGetComponent(out AudioSource audioSource))
                {
                    audioSource.volume = 0f;
                    DOTween.To(() => audioSource.volume, x => audioSource.volume = x, allObjects[i].Proportion, 2);
                    audioSource.Play();
                }
            }
        }
    }

    private void EndCutscene()
    {
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, durationShow);
        StartCoroutine(WaitForShutdown());

        foreach(var obj in allObjects)
        {
            if(obj.PlaySound)
            {
                if(obj.MainObject.TryGetComponent(out AudioSource audioSource))
                {
                    DOTween.To(() => audioSource.volume, x => audioSource.volume = x, 0, durationShow);
                }
            }
        }
    }

    [Serializable]
    public class ObjectDetails
    {
        public GameObject MainObject;
        public float Proportion;
        public bool Scale;
        public bool PlaySound;
    }
}
