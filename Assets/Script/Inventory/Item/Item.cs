using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private TMP_Text nameItem;
    [SerializeField] private Image image;

    public ItemDetails Details {  get; private set; }

    public void Setup(ItemDetails details)
    {
        if (this.Details != null)
        {
            Debug.LogError("Item is busy");
            return;
        }

        this.Details = details;
        if(details.icon != null)
        {
            image.sprite = details.icon;
            image.color = Color.white;
            return;
        }
        nameItem.text = details.name;
    }
}
