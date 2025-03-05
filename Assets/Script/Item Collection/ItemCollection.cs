using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
    public static ItemCollection Instance;

    [SerializeField] Movement playerMove;
    [SerializeField] CanvasGroup canvasG;
    [SerializeField] ItemInCollection prefabIII;
    [SerializeField] Transform contentIII;
    [SerializeField] CanvasGroup canvNewItem;
    [SerializeField] Image iconNewItem;
    [SerializeField] TMP_Text textNewItem;

    private InteractItem interactItemSelected;

    public bool IsOpen { private set; get; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && interactItemSelected == null)
        {
            if (IsOpen)
            {
                HideItemCollection();
            }
            else
                ShownItemCollection();
        }
    }

    public void ShownItemCollection(InteractItem iii = null)
    {
        canvasG.alpha = 1;
        canvasG.interactable = true;
        canvasG.blocksRaycasts = true;
        playerMove.SetCursorVisible(true);
        IsOpen = true;

        if (iii != null)
        {
            this.interactItemSelected = iii;
            Debug.Log(iii.Key.NameItem);
        }

        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void HideItemCollection()
    {
        canvasG.alpha = 0;
        canvasG.interactable = false;
        canvasG.blocksRaycasts = false;
        playerMove.SetCursorVisible(false);
        IsOpen = false;
        interactItemSelected = null;

        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public bool CheckIsItemCorrect(ItemFromCollection ifc) 
    {
        if (interactItemSelected == null)
            return false;

        if (interactItemSelected.Key == ifc)
        {
            Debug.Log("open");
            interactItemSelected.CorrectItem();
            interactItemSelected = null;
            return true;
        }
        else
            return false;
    }

    public void AddItem(ItemFromCollection ifc)
    {
        ItemInCollection iC = Instantiate(prefabIII, contentIII.transform);
        iC.Setup(ifc);
        StartCoroutine(ShowNewItem(ifc));
    }

    private IEnumerator ShowNewItem(ItemFromCollection ifc)
    {
        float elapsedTime = 0f;
        iconNewItem.sprite = ifc.iconItem;
        iconNewItem.color = ifc.colorItem;
        textNewItem.text = ifc.NameItem;
        canvNewItem.blocksRaycasts = true;
        AudioListener.pause = true;
        Movement.Instance.SetCursorVisible(true);

        while (elapsedTime < 1)
        {
            canvNewItem.alpha = Mathf.Lerp(0f, 1f, elapsedTime / 0.12f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Time.timeScale = 0f;
        canvNewItem.interactable = true;
        canvNewItem.alpha = 1f;
    }

    public void HideNewItem()
    {
        Time.timeScale = 1f;
        canvNewItem.alpha = 0f;
        AudioListener.pause = false;
        canvNewItem.interactable = false;
        canvNewItem.blocksRaycasts = false;
        Movement.Instance.SetCursorVisible(false);
    }
}
