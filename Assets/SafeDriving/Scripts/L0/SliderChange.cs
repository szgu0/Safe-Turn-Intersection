using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class SliderChange : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tmpTextValue;
    // Start is called before the first frame update
    public void ChangeSpeed(float text)
    {
        tmpTextValue.text = $"{text}";
    }
}
