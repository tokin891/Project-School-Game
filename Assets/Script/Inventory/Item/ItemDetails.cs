using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName ="Inventory/CreateItem")]
public class ItemDetails : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite icon;
}
