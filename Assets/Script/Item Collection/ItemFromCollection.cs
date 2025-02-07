using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Defualt Item", menuName ="Item/CollectionItem")]
public class ItemFromCollection : ScriptableObject
{
    public string NameItem;
    public string DescItem;
    public Sprite iconItem;
    public Color32 colorItem = Color.white;
}
