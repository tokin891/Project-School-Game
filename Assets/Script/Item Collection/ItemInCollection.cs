using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInCollection : MonoBehaviour
{
    [SerializeField] private ItemFromCollection ifc;
    [SerializeField] private TMP_Text nameT;
    [SerializeField] private TMP_Text descriptionT;
    [SerializeField] Image imageT;

    private ItemCollection ic;

    private void Awake()
    {
        ic = GetComponentInParent<ItemCollection>();
        if(ifc != null)
        {
            Setup(ifc);
        }
    }

    public void CheckItem()
    {
        if(ic.CheckIsItemCorrect(ifc))
        {
            DeleteItem();
        }
    }

    public void DeleteItem()
    {
        Destroy(gameObject);
    }

    public void Setup(ItemFromCollection ifc)
    {
        this.ifc = ifc;
        nameT.text = ifc.NameItem;
        descriptionT.text = ifc.DescItem;
        imageT.sprite = ifc.iconItem;
        imageT.color = ifc.colorItem;
    }
}
