using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        movement = orientation.right * Input.GetAxis("Horizontal") + orientation.forward * Input.GetAxis("Vertical");

        Look(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * Time.deltaTime * sensMouse);
    }

    void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    void MoveCharacter(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }

    void Look(Vector2 input)
    {
        yRot += input.x;
        xRot -= input.y;
        xRot = Mathf.Clamp(xRot, -90, 90);

        cam.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }
}
