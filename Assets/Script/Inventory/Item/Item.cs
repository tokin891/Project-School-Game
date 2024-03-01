using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    [SerializeField] private TMP_Text nameItem;

    public ItemDetails Details {  get; private set; }

    public void Setup(ItemDetails details)
    {
        if (this.Details != null)
        {
            Debug.LogError("Item is busy");
            return;
        }

        this.Details = details;
        nameItem.text = details.name;
    }
}
