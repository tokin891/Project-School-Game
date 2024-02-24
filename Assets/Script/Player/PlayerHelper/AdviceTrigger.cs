using UnityEngine;
using System.Collections;

public class AdviceTrigger : MonoBehaviour{
    private bool isPlayerInside = false;
    private Coroutine displayCoroutine;
    public AdviceManager adviceManager;
    public string[] messages;
    public float[] messageDurations;
    private int currentMessageIndex = 0;

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player") && !isPlayerInside){
            isPlayerInside = true;
            StartDisplayMessagesCoroutine();
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Player")){
            isPlayerInside = false;
            StopDisplayMessagesCoroutine();
        }
    }

    private void StartDisplayMessagesCoroutine(){
        if (displayCoroutine == null){
            displayCoroutine = StartCoroutine(DisplayMessagesCoroutine());
        }
    }

    private void StopDisplayMessagesCoroutine(){
        if (displayCoroutine != null){
            StopCoroutine(displayCoroutine);
            displayCoroutine = null;
        }
    }

    private IEnumerator DisplayMessagesCoroutine(){
        while (currentMessageIndex < messages.Length){
            float elapsedTime = 0f;
            while (elapsedTime < messageDurations[currentMessageIndex] && isPlayerInside){
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            if (isPlayerInside){
                adviceManager.DisplayMessage(messages[currentMessageIndex]);
                yield return new WaitForSeconds(adviceManager.fadeDuration);
                currentMessageIndex++;
            }
        }
        currentMessageIndex = 0;
    }
}