using System;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class FillBar
{
    [SerializeField] private Image _healthBar;

    public void SetAmount(float value)
    {
        value = Mathf.Clamp(value, 0, 1);
        _healthBar.fillAmount = value;
    }
}