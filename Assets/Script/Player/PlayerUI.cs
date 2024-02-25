using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] CanvasGroup canG;

    private void OnEnable()
    {
        canG.alpha = 1f;
        DOTween.To(() => canG.alpha, x => canG.alpha = x, 0, 2);
    }
}
