using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSlider : MonoBehaviour
{
    public Slider staminaSlider;

    private void Awake()
    {
        staminaSlider = GetComponentInChildren<Slider>();
    }

    public void SetSliderValue(float value)
    {
        staminaSlider.value = value;
    }

    private void OnEnable()
    {
        ArrowHolder.OnStaminaChange += SetSliderValue;
    }

    private void OnDisable()
    {
        ArrowHolder.OnStaminaChange -= SetSliderValue;
    }
}
