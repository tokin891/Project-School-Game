using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdviceManager : MonoBehaviour{
    public Text adviceText;
    public GameObject adviceObject;
    public float fadeDuration = 1f;

    public void DisplayMessage(string message){
        StartCoroutine(DisplayMessageCoroutine(message));
    }

    private IEnumerator DisplayMessageCoroutine(string message){
        adviceText.text = message;
        adviceObject.SetActive(true);
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(fadeDuration);
        yield return StartCoroutine(FadeOut());
        adviceObject.SetActive(false);
    }

    private IEnumerator FadeIn(){
        float elapsedTime = 0f;
        Color textColor = adviceText.color;

        while (elapsedTime < fadeDuration){
            textColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            adviceText.color = textColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textColor.a = 1f;
        adviceText.color = textColor;
    }

    private IEnumerator FadeOut(){
        float elapsedTime = 0f;
        Color textColor = adviceText.color;
        while (elapsedTime < fadeDuration){
            textColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            adviceText.color = textColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textColor.a = 0f;
        adviceText.color = textColor;
    }
}