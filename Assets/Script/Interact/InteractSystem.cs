using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class InteractSystem : MonoBehaviour
{
    [SerializeField] private float range = 4f;
    [SerializeField] private Crosshair crosshair;

    private IInteract interactObject;

    private void Update()
    {
        interactObject = GetObject();

        if(interactObject != null)
        {
            crosshair.SetColor(Color.green);
        }else
        {
            crosshair.SetColor(Color.white);
        }
    }

    private IInteract GetObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            if(hit.transform.TryGetComponent(out IInteract _interactObject))
            { return _interactObject; }
        }

        return null;
    }

    public void TryInteract(InputAction.CallbackContext context)
    {
        if (context.started == false)
        {
            return;
        }
        if (interactObject != null) { interactObject.CameraInteractWithObject(); }
    }
}
