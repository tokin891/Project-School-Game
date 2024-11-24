using UnityEngine;
using System.Collections;

public class AdviceTrigger : MonoBehaviour{
    private bool isPlayerInside = false;
    public string messages;
    public float messageDuration;

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player") && !isPlayerInside){
            isPlayerInside = true;
            AdviceManager.Instance.DisplayMessage(messages, messageDuration);
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Player")){
            isPlayerInside = false;
        }
    }
}