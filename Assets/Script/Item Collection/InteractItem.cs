using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractItem:MonoBehaviour
{
    public ItemFromCollection Key;
    public abstract void CorrectItem();
}
