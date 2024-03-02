using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour
{
    private Image image;
    [SerializeField] Color colorNotUsable;
    [SerializeField] Color colorUsable;

    public Item CurrentItem {  get; private set; }
    public bool IsInUse {  get; private set; }
    public bool IsBusy { get; private set; }

    private const float scale = 0.71716f;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Setup(Item item)
    {
        if(this.CurrentItem != null)
        {
            Debug.LogError("Item holder is busy");
            return;
        }

        this.CurrentItem = item;
        IsBusy = true;

        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = new Vector3 (scale, scale, scale);
    }

    public void Clear()
    {
        if (this.CurrentItem == null)
        {
            Debug.LogError("Item holder is null");
            return;
        }

        Destroy(this.CurrentItem.gameObject);
        CurrentItem = null;
    }

    public void SetUsable(bool usable)
    {
        if(image == null)
        {
            WaitForImage(usable);
            return;
        }

        IsInUse = usable;

        if(usable)
        {
            image.color = colorUsable;
        }else
        {
            image.color = colorNotUsable;
        }
    }

    private IEnumerator WaitForImage(bool usable)
    {
        while(image == null)
            yield return null;
        SetUsable(usable);
    }
}
