using DG.Tweening;
using Google.Protobuf.WellKnownTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChairSystem : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private float maxAngelCameraRotate = 30f;
    [SerializeField] private float maxRatioEscape = 100;
    [SerializeField] private float addRatioEscape = 15;
    [SerializeField] CanvasGroup blackScreem;
    [SerializeField] GameObject player;
    [SerializeField] GameObject trapChair;

    private Vector2 input;
    private Transform chair;
    private Animator animator;
    private float xRot;
    private float sensitivity;
    private float ratioEscape;
    private bool isLeftSide;
    private bool isEnd;

    private void Awake()
    {
        chair = GetComponent<Transform>();
        animator = GetComponent<Animator>();

        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            sensitivity = PlayerPrefs.GetFloat("Sensitivity");
        }
        else
            sensitivity = 100;
    }

    public void GetInputMouse(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        xRot += input.x * Time.deltaTime * sensitivity;
        xRot = Mathf.Clamp(xRot,-maxAngelCameraRotate,maxAngelCameraRotate);
        cam.localRotation = Quaternion.Slerp(cam.localRotation, Quaternion.Euler(0,xRot,0), 6 * Time.deltaTime);
    }

    public void ShakeLeft(InputAction.CallbackContext context)
    {
        if (isEnd)
            return;
        if(isLeftSide)
        {
            return;
        }

        animator.SetTrigger("Left");
    }

    public void ShakeRight(InputAction.CallbackContext context)
    {
        if (isEnd)
            return;
        if (!isLeftSide)
        {
            return;
        }

        animator.SetTrigger("Right");
    }

    public void ResetTrigger()
    {
        animator.ResetTrigger("Left");
        animator.ResetTrigger("Right");
    }

    public void AddRatioShakeLeft()
    {
        isLeftSide = true;
        AddRatio();
    }

    public void AddRatioShakeRight()
    {
        isLeftSide = false;
        AddRatio();
    }

    private void AddRatio()
    {
        ratioEscape += addRatioEscape;

        if(ratioEscape >= maxRatioEscape)
        {
            animator.SetTrigger("End");
            isEnd = true;
            StartCoroutine(AfterEnd());
        }
    }

    private IEnumerator AfterEnd()
    {
        yield return new WaitForSeconds(5);

        DOTween.To(() => blackScreem.alpha, x => blackScreem.alpha = x, 1, 2);
        yield return new WaitForSeconds(2);
        player?.SetActive(true);
        gameObject.SetActive(false);
        trapChair?.SetActive(true);
        GameManagerBasement.Instance.ChangeStateOfGameByInt(3);
    }
}
