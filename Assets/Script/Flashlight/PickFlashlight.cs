using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickFlashlight : MonoBehaviour, IInteract
{
    [SerializeField] private ItemDetails flashlightDetails;

    public void CameraInteractWithObject()
    {
        if (Movement.Instance.Hand != null)
        {
            StartCoroutine(MoveToPlayerHandWithSlowRotation());
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
        else
        {
            Debug.LogWarning("Attach hand");
        }
    }

    private IEnumerator MoveToPlayerHandWithSlowRotation()
    {
        float elapsedTime = 0f;
        float duration = 1.0f;

        Vector3 initialPosition = transform.position;
        Quaternion initialRotation = transform.rotation;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(initialPosition, Movement.Instance.Hand.position, t);
            transform.rotation = Quaternion.Slerp(initialRotation, Movement.Instance.Hand.rotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set in inventory
        InventoryManager.Instance.TryAddItem(flashlightDetails);
        Destroy(gameObject);
    }
}
