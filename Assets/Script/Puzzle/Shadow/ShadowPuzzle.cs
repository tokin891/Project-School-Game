using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShadowPuzzle : MonoBehaviour
{
    [SerializeField] Transform figure;
    [SerializeField] Transform point;
    [SerializeField] float radius;
    [SerializeField] float speed;

    private float angle;
    private bool stopAction;
    private Quaternion awakeRot;
    private Vector2 moveFigure;

    private void OnEnable()
    {
        awakeRot = figure.localRotation;
        int rX = Random.Range(0, 359);
        int rY = Random.Range(0, 359);

        figure.transform.localRotation = Quaternion.Euler(rX, rY, 0);
    }

    private void Update()
    {
        if (stopAction)
        {
            figure.transform.localRotation = Quaternion.Slerp(figure.transform.localRotation, awakeRot, 10 * Time.deltaTime);
            return; 
        }

        Vector3 targetDir = point.position - figure.position;
        angle =Vector3.Angle(targetDir, figure.transform.forward);

        figure.Rotate(new Vector2(moveFigure.x, moveFigure.y) * speed * Time.deltaTime);
    }

    public void CheckAnserw(InputAction.CallbackContext callbackContext)
    {
        if (angle <= radius || ((angle - 175 <= radius) && (angle - 175 > 0)))
        {
            GoodAnserw();
        }

        Debug.Log("CheckAnserw");
    }

    public void MoveFigure(InputAction.CallbackContext context)
    {
        moveFigure = context.ReadValue<Vector2>();
    }

    private void GoodAnserw()
    {
        stopAction = true;       
    }
}
