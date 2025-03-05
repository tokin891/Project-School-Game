using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour, IInteract
{
    public bool isLocked;
    public bool isOpen;

    [SerializeField] private float smooth = 10f;
    [SerializeField] private float multiplayer = 1f;

    private Quaternion currentRot;
    private Quaternion destinationRot;
    private float delay;
    private bool isOpening = false;
    private NavMeshObstacle Obstacle;


    private void Awake()
    {
        destinationRot = transform.localRotation;
        if(TryGetComponent<NavMeshObstacle>(out Obstacle))
        {
            Obstacle.carveOnlyStationary = false;
            Obstacle.carving = isOpen;
            Obstacle.enabled = isOpen;
        }
    }

    public void CameraInteractWithObject()
    {
        if(isLocked) return;
        if (delay > Time.time)
        {
            return;
        }
        delay = Time.time + 1f;
        if(isOpen) { Close(); return; }
        if(!isOpen ) { Open(); return; }
    }

    private void Update()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, destinationRot, smooth * Time.deltaTime);

        if (isOpening && Quaternion.Angle(transform.localRotation, destinationRot) < 2.5f)
        {
            isOpening = false;
        }
    }

    public void Open()
    {
        if (isOpen)
            return;
        currentRot = transform.localRotation;
        destinationRot = Quaternion.Euler(currentRot.eulerAngles.x, currentRot.eulerAngles.y + 90f * multiplayer, currentRot.eulerAngles.z);
        isOpening = true;
        if(Obstacle != null)
        {
            Obstacle.enabled = true;
            Obstacle.carving = true;
        }
        Debug.Log("open door");

        isOpen = true;
    }

    private void Close()
    {
        if (!isOpen)
            return;

        if (Obstacle != null)
        {
            Obstacle.enabled = false;
            Obstacle.carving = false;
        }
        currentRot = transform.localRotation;
        destinationRot = Quaternion.Euler(currentRot.eulerAngles.x, currentRot.eulerAngles.y + -90f * multiplayer, currentRot.eulerAngles.z);
        Debug.Log("close door");

        isOpen = false;
    }
}
