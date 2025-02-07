using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class AdviceManager : MonoBehaviour{
    public static AdviceManager Instance;
    [SerializeField] TMP_Text adviceText;
    [SerializeField] GameObject adviceObject;
    [SerializeField] float fadeDuration;

    private void Awake()
    {
        Instance = this;
    }

    public void DisplayMessage(string message, float textDuration){
        StartCoroutine(DisplayMessageCoroutine(message, textDuration));
    }
    public void DisplayMessage(string message)
    {
        StartCoroutine(DisplayMessageCoroutine(message, 5f));
    }

    private IEnumerator DisplayMessageCoroutine(string message, float textDuration){
        adviceText.text = message;
        adviceObject.SetActive(true);
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(textDuration);
        yield return StartCoroutine(FadeOut());
        adviceObject.SetActive(false);
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color textColor = adviceText.color;

        while (elapsedTime < fadeDuration)
        {
            textColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            adviceText.color = textColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textColor.a = 1f;
        adviceText.color = textColor;
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color textColor = adviceText.color;
        while (elapsedTime < fadeDuration)
        {
            textColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            adviceText.color = textColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textColor.a = 0f;
        adviceText.color = textColor;
    }
}