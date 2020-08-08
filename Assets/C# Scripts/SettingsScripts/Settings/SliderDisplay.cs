using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderDisplay : MonoBehaviour
{
    public Text text;
    public bool showPercents;

    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.onValueChanged.AddListener(UpdateDisplayText);
        UpdateDisplayText(slider.value);
    }

    public void UpdateDisplayText(float value)
    {
        text.text = showPercents ? (value / slider.maxValue).ToString("P0") : value.ToString();
    }
}
