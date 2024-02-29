using UnityEngine;
using System.Collections;

public class InteractPadlock : MonoBehaviour, IInteract{
    [SerializeField] public GameObject characterToDisable;
    [SerializeField] public PinManager pinManager;
    [SerializeField] public GameObject mainCamera;
    [SerializeField] public BoxCollider boxColliderToDisable;

    private bool isInPinEntry = false;

    private void OnEnable(){
        PinManager.OnCorrectPinEntered += OnCorrectPinEnteredHandler;
    }

    private void OnDisable(){
        PinManager.OnCorrectPinEntered -= OnCorrectPinEnteredHandler;
    }

    private void OnCorrectPinEnteredHandler(){
        if (isInPinEntry){
            ExitPinEntry();
        }
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            ExitPinEntry();
        }
    }

    private void ExitPinEntry(){
        if (characterToDisable != null){
            characterToDisable.SetActive(true);
        }

        if (pinManager != null){
            pinManager.enabled = false;
        }

        if (mainCamera != null){
            mainCamera.SetActive(false);
        }

        if (boxColliderToDisable != null){
            boxColliderToDisable.enabled = true;
        }
        isInPinEntry = false;
    }

    public void CameraInteractWithObject(){
        isInPinEntry = true;

        if (characterToDisable != null){
            characterToDisable.SetActive(false);
        }

        if (pinManager != null){
            pinManager.enabled = true;
        }

        if (mainCamera != null){
            mainCamera.SetActive(true);
        }

        if (boxColliderToDisable != null){
            boxColliderToDisable.enabled = false;
        }
    }
}
