using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.InputSystem;
using UnityEditor.ShaderGraph.Internal;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public static Movement Instance;
    [SerializeField] private float sensMouse = 100f;
    private Rigidbody rb;
    private Vector3 movement;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform crouchCamera;
    [SerializeField] private float speedNormal;
    [SerializeField] private float speedRunning;
    [SerializeField] private float speedCrouching;
    [SerializeField] private Transform heightUnderHead;
    [SerializeField] private Vector3 customGravity;
    [SerializeField] private bool hideCursor = true;
    [SerializeField] private float delayFootstepNormal = 0.85f;
    [SerializeField] private float delayFootstepCrouch = 1.2f;
    [SerializeField] private AudioSource fs;

    private float speed;
    private float xRot;
    private float yRot;
    private Vector2 inputMouse;
    private Vector2 inputMove;
    private Vector3 cameraAwakePos;
    private Vector3 cameraTarget;
    private CapsuleCollider capsuleCollider;
    private bool crouchingNotDone;
    private bool isCrouching;
    private bool cantMoveMouse;
    private float nextTimeToPlayFootstep;
    private bool isMoving;

    public Transform Hand;

    public bool IsCrouching { get; private set; }
    public bool IsRunning { get; private set; }

    void Start()
    {
        Instance = this;
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

        SetCursorVisible(!hideCursor);
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
        if(!IsSomethingUnderHead() && !isCrouching)
        {
            SetCrouching(false);
        }

        if(isMoving)
        {
            if (nextTimeToPlayFootstep < Time.time)
            {
                fs.Play();

                nextTimeToPlayFootstep = IsCrouching ? Time.time + delayFootstepCrouch : Time.time + delayFootstepNormal;
            }
        }
    }

    void FixedUpdate()
    {
        MoveCharacter(movement);
        rb.velocity += customGravity * Time.deltaTime;
    }

    void MoveCharacter(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }

    void SetCrouching(bool crouching)
    {
        if(crouching)
        {
            cameraTarget = crouchCamera.transform.localPosition;
            capsuleCollider.height = 1f;
            IsCrouching = true;
        }
        else
        {
            capsuleCollider.height = 2.45f;
            cameraTarget = cameraAwakePos;
            IsCrouching = false;
            speed = speedNormal;
        }
    }

    public void Look(InputAction.CallbackContext context)
    {
        if (cantMoveMouse)
        {
            inputMouse = Vector2.zero;
            return;
        }
        inputMouse = context.ReadValue<Vector2>() * sensMouse * Time.deltaTime;
    }

    public void Move(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();

        if(context.performed)
        {
            isMoving = true;
        }

        if(context.canceled)
            isMoving = false;
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isCrouching = true;
            SetCrouching(true);
            speed = speedCrouching;
        }
        if(context.canceled)
        {
            isCrouching = false;

            if(IsSomethingUnderHead() == false)
            {
                SetCrouching(false);
            }
        }
    }

    public void Run(InputAction.CallbackContext context)
    {
        if (isCrouching)
            return;
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

    private bool IsSomethingUnderHead()
    {
        return Physics.CheckSphere(heightUnderHead.position, 0.25f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(heightUnderHead.position, 0.25f);
    }

    public void SetCursorVisible(bool value)
    {
        if (value)
        {
            Cursor.lockState = CursorLockMode.None;
            cantMoveMouse = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            cantMoveMouse = false;
        }
    }
}
