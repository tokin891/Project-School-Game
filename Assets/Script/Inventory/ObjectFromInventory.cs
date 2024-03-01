using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFromInventory : MonoBehaviour
{
    public ItemDetails Details;
    [SerializeField] private GameObject objectF;

    public void Visible(bool vis) => objectF.SetActive(vis);
}
