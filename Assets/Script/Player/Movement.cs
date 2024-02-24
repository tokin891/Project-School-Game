using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float sensMouse = 100f;
    private Rigidbody rb;
    private Vector3 movement;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform crouchCamera;
    [SerializeField] private float speedNormal;
    [SerializeField] private float speedRunning;

    private float speed;
    private float xRot;
    private float yRot;
    private Vector2 inputMouse;
    private Vector2 inputMove;
    private Vector3 cameraAwakePos;
    private Vector3 cameraTarget;
    private CapsuleCollider capsuleCollider;

    public bool IsCrouching {  get; private set; }
    public bool IsRunning { get; private set; }

    void Start()
    {
        speed = speedNormal;
        cameraAwakePos = cameraHolder.transform.localPosition;
        cameraTarget = cameraAwakePos;
        rb = this.GetComponent<Rigidbody>();

        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            UpdateSensitivity(PlayerPrefs.GetFloat("Sensitivity"));
        }
        else
            UpdateSensitivity(100);

        capsuleCollider = GetComponent<CapsuleCollider>();
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        movement = orientation.right * inputMove.x + orientation.forward * inputMove.y;

        yRot += inputMouse.x;
        xRot -= inputMouse.y;
        xRot = Mathf.Clamp(xRot, -90, 90);
        cam.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);

        cameraHolder.transform.localPosition = Vector3.Lerp(cameraHolder.localPosition, cameraTarget, 7.5f * Time.deltaTime);
    }

    void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    void MoveCharacter(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }

    public void Look(InputAction.CallbackContext context)
    {
        inputMouse = context.ReadValue<Vector2>() * sensMouse * Time.deltaTime; ;
    }

    public void Move(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            IsCrouching = true;
            cameraTarget = crouchCamera.transform.localPosition;
            capsuleCollider.height = 1.25f;
        }
        if(context.canceled)
        {
            IsCrouching = false;
            cameraTarget = cameraAwakePos;
            capsuleCollider.height = 2f;
        }
    }

    public void Run(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsRunning = true;
            speed = speedRunning;
        }
        if (context.canceled)
        {
            IsRunning = false;
            speed = speedNormal;
        }
    }

    public void UpdateSensitivity(float sensitivity)
    {
        sensMouse = sensitivity;
    }
}
