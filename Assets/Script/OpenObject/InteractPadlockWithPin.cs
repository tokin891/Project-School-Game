using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class InteractPadlockWithPin : MonoBehaviour, IInteract{
    [SerializeField] private GameObject characterToDisable;
    [SerializeField] private PinManager padlockWithPin;
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

        if (padlockWithPin != null){
            padlockWithPin.enabled = false;
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

        if (padlockWithPin != null){
            padlockWithPin.enabled = true;
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
