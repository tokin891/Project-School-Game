using UnityEngine;
using System.Collections;

public class AdviceTrigger : MonoBehaviour{
    private bool isPlayerInside = false;
    private float timeInsideTrigger = 0f;
    public AdviceManager adviceManager;
    public string[] messages;
    public float[] messageDurations;
    private int currentMessageIndex = 0;
    private float fadeDuration;

    private void Start(){
        fadeDuration = adviceManager.fadeDuration;
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player") && !isPlayerInside){
            isPlayerInside = true;
            StartCoroutine(DisplayMessagesCoroutine());
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

private IEnumerator DisplayMessagesCoroutine(){
    while (currentMessageIndex < messages.Length){
        float elapsedTime = 0f;
        bool displayedMessage = false;
        while (elapsedTime < messageDurations[currentMessageIndex] && isPlayerInside){
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (isPlayerInside && !displayedMessage){
            adviceManager.DisplayMessage(messages[currentMessageIndex]);
            displayedMessage = true;
            yield return new WaitForSeconds(fadeDuration);
            currentMessageIndex++;
            while (isPlayerInside){
                yield return null;
            }
            while (!isPlayerInside)
            {
                yield return null;
            }
            timeInsideTrigger = 0f;
        }
    }
    currentMessageIndex = 0;
}
}
