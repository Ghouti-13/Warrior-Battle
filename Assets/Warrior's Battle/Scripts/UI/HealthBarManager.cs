using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private Canvas _healthBarUI;
    [SerializeField] private Slider _healthSlider;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        transform.LookAt(_healthBarUI.transform.position + _mainCamera.transform.rotation * Vector3.back, _mainCamera.transform.rotation * Vector3.up);
    }
    public void SetMaxHealth(float value)
    {
        _healthSlider.maxValue = value;
        _healthSlider.value = value;
    }
    public void SetHealthValue(float value)
    {
        _healthSlider.value = value;
    }
}
