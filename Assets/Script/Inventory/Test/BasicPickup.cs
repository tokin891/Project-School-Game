using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPickup : MonoBehaviour, IInteract
{
    public ItemDetails pickup;

    public void CameraInteractWithObject()
    {
        if(InventoryManager.Instance.TryAddItem(pickup))
        {
            Destroy(gameObject);
        }
    }
}
