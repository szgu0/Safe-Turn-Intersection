using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedSliderData : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sliderData;

    public void toChangeData()
    {
        sliderData.text = slider.value.ToString();

    }
    

}
