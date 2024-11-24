using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] private ItemHolder[] allSlots;
    [SerializeField] private Item prefabItem;
    [SerializeField] private Transform objectsInHand;
    [SerializeField] private Transform highlightCurrentItem;

    private ObjectFromInventory[] objectsFromInventory;
    private int currentUseItemHolder;
    private CanvasGroup canGroup;

    private float timeToHideInv;

    private void Awake()
    {
        canGroup = GetComponent<CanvasGroup>();
        canGroup.alpha = 0;
        Instance = this;
        objectsFromInventory = objectsInHand.GetComponentsInChildren<ObjectFromInventory>();
        UseHolder(0);
    }

    private void Update()
    {
        if(timeToHideInv < Time.time)
        {
            DOTween.To(() => canGroup.alpha, x => canGroup.alpha = x, 0, 1.5f);
        }else
        {
            DOTween.To(() => canGroup.alpha, x => canGroup.alpha = x, 1, 1.5f);
        }
    }

    private void FixedUpdate()
    {
        highlightCurrentItem.position = Vector2.Lerp(highlightCurrentItem.position, allSlots[currentUseItemHolder].transform.position, 10 * Time.deltaTime);
    }

    private void UseHolder(int holder)
    {
        currentUseItemHolder = holder;

        for (int i = 0; i < allSlots.Length; i++)
        {
            allSlots[i].SetUsable(false);
        }

        allSlots[holder].SetUsable(true);
        if (allSlots[holder].CurrentItem != null)
        {
            ShowItem(allSlots[holder].CurrentItem.Details);
        }else
        {
            for (int i = 0; i < objectsFromInventory.Length; i++)
            {
                objectsFromInventory[i].Visible(false);
            }
        }
        ShowInventory(4);
    }

    public bool TryAddItem(ItemDetails itemDetails)
    {
        for (int i = 0; i < allSlots.Length; i++)
        {
            if (allSlots[i].IsBusy == false)
            {
                Item newItem = Instantiate(prefabItem);
                newItem.Setup(itemDetails);
                allSlots[i].Setup(newItem);
                UseHolder(i);

                return true;
            }
        }

        return false;
    }

    public void NextSlot(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        if(currentUseItemHolder + 1 > allSlots.Length-1)
        {
            UseHolder(0);
            return;
        }

        UseHolder(currentUseItemHolder + 1);
    }

    public void PreviousSlot(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        if (currentUseItemHolder - 1 < 0)
        {
            UseHolder(allSlots.Length-1);
            return;
        }

        UseHolder(currentUseItemHolder - 1);
    }

    private void ShowInventory(float delay)
    {
        timeToHideInv = Time.time + delay;
    }

    public void ShowItem(ItemDetails itemDetails)
    {
        if(itemDetails == null)
        {
            return;
        }

        for (int i = 0; i < objectsFromInventory.Length; i++)
        {
            objectsFromInventory[i].Visible(false);
        }
        foreach (var item in objectsFromInventory)
        {
            if(item.Details.Name == itemDetails.Name)
            {
                item.Visible(true);
                Debug.Log(item.Details.Name);
            }
        }
    }
}
