using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUIHandle : MonoBehaviour
{
    [SerializeField] private Slider _bossHealthSlider;
    private BossHealth _bossHealth;

    private void OnEnable()
    {
        _bossHealth = FindObjectOfType<BossHealth>();
        _bossHealthSlider.maxValue = _bossHealth.MaxHealth;
        _bossHealthSlider.value = _bossHealth.CurrentHealth;
    }

    private void Update()
    {
        if (_bossHealth == null) return;

        
        _bossHealthSlider.maxValue = _bossHealth.MaxHealth;
        _bossHealthSlider.value = _bossHealth.CurrentHealth;
    }


}
