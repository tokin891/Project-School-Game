using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChainOpen : InteractItem, IInteract
{
    private Collider colider;
    [SerializeField] private GameObject characterToDisable;
    [SerializeField] ItemCollection itemCollection;
    [SerializeField] private Camera cam;
    private bool isOpen;
    private bool isEnding;

    private void Awake()
    {
        colider = GetComponent<Collider>();
    }

    public void CameraInteractWithObject()
    {
        GoTroughtChains();
    }

    public override void CorrectItem()
    {
        GetComponent<Animator>().SetTrigger("open");
        itemCollection.HideItemCollection();
        isEnding = true;
        Debug.Log("END");
    }

    public void EndAnimation()
    {
        colider.isTrigger = true;
        Destroy(colider);
        cam.gameObject.SetActive(false);
        ExitChains();
    }

    public void ShowCollection()
    {
        itemCollection.ShownItemCollection(this);
    }

    public void ExitChains()
    {
        if (characterToDisable != null)
        {
            characterToDisable.SetActive(true);
        }

        isOpen = false;
        cam.gameObject.SetActive(false);
    }

    private void GoTroughtChains()
    {
        if (characterToDisable != null)
        {
            characterToDisable.SetActive(false);
        }

        ShowCollection();
        isOpen = true;
        cam.gameObject.SetActive(true);
    }

    public void Stop(InputAction.CallbackContext context)
    {
        if (!isOpen || isEnding)
            return;

        itemCollection.HideItemCollection();
        ExitChains();
    }
}
