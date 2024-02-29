using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class InteractPadlock : MonoBehaviour, IInteract{
    [SerializeField] private GameObject characterToDisable;
    [SerializeField] private PinManager pinManager;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private BoxCollider boxColliderToDisable;
    [SerializeField] private GameObject inputSystem;

    private bool isInPinEntry = false;

    private void OnEnable(){
        PinManager.OnCorrectPinEntered += OnCorrectPinEnteredHandler;
        inputSystem.SetActive(false);
    }

    private void OnDisable(){
        PinManager.OnCorrectPinEntered -= OnCorrectPinEnteredHandler;
    }

    private void OnCorrectPinEnteredHandler()
    {
        if (isInPinEntry)
        {
            ExitPinEntry();
        }
    }

    public void TryExit(InputAction.CallbackContext context)
    {
        ExitPinEntry();
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
        inputSystem.SetActive(false);
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

        inputSystem.SetActive(true);
    }
}
