using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    [SerializeField] Movement playerMove;

    private CanvasGroup canvasG;

    public bool IsOpen { private set; get; }

    private void Awake()
    {
        canvasG = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (IsOpen)
            {
                HideItemCollection();
            }
            else
                ShownItemCollection();
        }
    }

    private void ShownItemCollection()
    {
        DOTween.To(() => canvasG.alpha, x => canvasG.alpha = x, 1, 1.5f);
        canvasG.interactable = true;
        canvasG.blocksRaycasts = true;
        playerMove.SetCursorVisible(true);
        IsOpen = true;
    }

    private void HideItemCollection()
    {
        DOTween.To(() => canvasG.alpha, x => canvasG.alpha = x, 0, 1.5f);
        canvasG.interactable = false;
        canvasG.blocksRaycasts = false;
        playerMove.SetCursorVisible(false);
        IsOpen = false;
    }
}
