using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdviceManager : MonoBehaviour{
    public static AdviceManager Instance;
    [SerializeField] Text adviceText;
    [SerializeField] GameObject adviceObject;
    [SerializeField] float fadeDuration;

    private void Awake()
    {
        Instance = this;
    }

    public void DisplayMessage(string message, float textDuration){
        StartCoroutine(DisplayMessageCoroutine(message, textDuration));
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