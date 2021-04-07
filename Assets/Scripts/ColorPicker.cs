using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    Slider hueSlider;

    private void Awake()
    {
        hueSlider = GetComponent<Slider>();
    }

    public void HueChanged()
    {
        Color c = Color.HSVToRGB(hueSlider.value, 1f, 1f);
        Debug.Log(c.r);
    }
}
