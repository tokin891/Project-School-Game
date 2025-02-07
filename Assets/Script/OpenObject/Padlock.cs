using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Padlock : InteractItem, IInteract
{
    [SerializeField] private GameObject characterToDisable;
    [SerializeField] ItemCollection itemCollection;
    [SerializeField] private Door door;
    [SerializeField] private Camera cam;
    private bool isOpen;
    private bool isEnding;

    private void Awake()
    {
        door.isLocked = true;
        door.enabled = false;
    }

    public void ShowCollection()
    {
        itemCollection.ShownItemCollection(this);
    }

    public void Open()
    {
        // Open Door
        door.enabled = true;
        isEnding = true;
        GetComponent<Animator>().SetTrigger("open");
        itemCollection.HideItemCollection();
    }

    public void Stop(InputAction.CallbackContext context)
    {
        if (!isOpen || isEnding)
            return;

        itemCollection.HideItemCollection();
        ExitPadlock();
    }

    public void CameraInteractWithObject()
    {
        GoTroughtPadlock();
    }

    private void GoTroughtPadlock()
    {
        if (characterToDisable != null)
        {
            characterToDisable.SetActive(false);
        }

        ShowCollection();
        isOpen = true;
        cam.gameObject.SetActive(true);
    }

    public void ExitPadlock()
    {
        if (characterToDisable != null)
        {
            characterToDisable.SetActive(true);
        }

        isOpen = false;
        cam.gameObject.SetActive(false);
    }

    public void EndAnimation()
    {
        door.isLocked = false;
        ExitPadlock();
        Destroy(gameObject);
    }

    public override void CorrectItem()
    {
        Open();
    }
}
