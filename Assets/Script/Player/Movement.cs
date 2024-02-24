using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float sensMouse = 100f;
    private Rigidbody rb;
    private Vector3 movement;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform orientation;

    private float xRot = 0;
    private float yRot = 0;
    private Vector2 inputMouse;
    private Vector2 inputMove;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        float sens = PlayerPrefs.GetFloat("Sensitivity");
        if (sens == 0)
        {
            UpdateSensitivity(100f);
        }
        else
            UpdateSensitivity(sens);
    }

    void Update()
    {
        movement = orientation.right * inputMove.x + orientation.forward * inputMove.y;

        yRot += inputMouse.x;
        xRot -= inputMouse.y;
        xRot = Mathf.Clamp(xRot, -90, 90);

        cam.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }

    void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    void MoveCharacter(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        inputMouse = context.ReadValue<Vector2>() * sensMouse * Time.deltaTime;;
    }

    public void UpdateSensitivity(float sensitivity)
    {
        sensMouse = sensitivity;
    }
}
