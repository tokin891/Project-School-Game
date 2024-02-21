using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdviceManager : MonoBehaviour{
    public Text adviceText;
    public GameObject adviceObject;
    public float fadeDuration = 1f; //czas FADEIN i FADEOUT i czas przerwy pomiedzy nimi
    public string[] messages;
    public float[] messageDurations;

    private bool isPlayerInside = false;
    private int currentMessageIndex = 0;
    private float timeInsideTrigger = 0f;

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player") && !isPlayerInside){ //Player TAG na graczu
            isPlayerInside = true;
            StartCoroutine(DisplayMessageCoroutine());
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Player")){
            isPlayerInside = false;
            timeInsideTrigger = 0f;
        }
    }

    private void Update(){
        if (isPlayerInside){
            timeInsideTrigger += Time.deltaTime;
        }
    }

    private IEnumerator DisplayMessageCoroutine(){
        while (currentMessageIndex < messages.Length){
            if (timeInsideTrigger >= messageDurations[currentMessageIndex]){
                adviceText.text = messages[currentMessageIndex];
                adviceObject.SetActive(true);

                yield return StartCoroutine(FadeIn());
                yield return new WaitForSeconds(fadeDuration);
                yield return StartCoroutine(FadeOut());

                adviceObject.SetActive(false);
                currentMessageIndex++;
                timeInsideTrigger = 0f;
            }

            yield return null;
        }
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
