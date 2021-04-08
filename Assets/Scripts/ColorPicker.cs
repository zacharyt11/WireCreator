using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    [SerializeField] Slider hueSlider;
    [SerializeField] Slider satSlider;
    [SerializeField] Slider valSlider;
    [SerializeField] Image valSpr;
    [SerializeField] Image prevImg;

    public Color GetColor()
    {
        return Color.HSVToRGB(hueSlider.value, satSlider.value, valSlider.value);
    }

    public void HueChanged()
    {
        valSpr.color = Color.HSVToRGB(hueSlider.value, 1f, 1f);
    }

    public void AnyChanged()
    {
        prevImg.color = GetColor();
    }
}
