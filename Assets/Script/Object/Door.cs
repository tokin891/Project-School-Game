using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    public bool isLocked;
    private bool isOpen;

    [SerializeField] private float smooth = 10f;
    [SerializeField] private float multiplayer = 1f;

    private Quaternion currentRot;
    private Quaternion destinationRot;
    private float delay;

    private void Awake()
    {
        destinationRot = transform.localRotation;
    }

    public void CameraInteractWithObject()
    {
        if(isLocked) return;
        if (delay > Time.time)
        {
            return;
        }
        delay = Time.time + 1f;
        if(isOpen) { Close(); }
        if(!isOpen ) { Open(); }

        isOpen = !isOpen;
    }

    private void Update()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, destinationRot, smooth * Time.deltaTime);
    }

    private void Open()
    {
        currentRot = transform.localRotation;
        destinationRot = Quaternion.Euler(currentRot.eulerAngles.x, currentRot.eulerAngles.y + 90f * multiplayer, currentRot.eulerAngles.z);
    }

    private void Close()
    {
        currentRot = transform.localRotation;
        destinationRot = Quaternion.Euler(currentRot.eulerAngles.x, currentRot.eulerAngles.y + -90f * multiplayer, currentRot.eulerAngles.z);
    }
}
