using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItemCollection : MonoBehaviour, IInteract
{
    [SerializeField] ItemFromCollection iii;

    public void CameraInteractWithObject()
    {
        ItemCollection.Instance.AddItem(iii);

        StartCoroutine(DestroyWithDelay());
    }

    IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
