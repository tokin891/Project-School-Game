using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Crosshair : MonoBehaviour
{
    public static Crosshair Instance;

    private Image image;

    private void Awake()
    {
        Instance = this;
        image = GetComponent<Image>();
    }

    public void SetColor(Color color)
    {
        if(image.color == color) { return; }
        image.color = color;
    }
}
