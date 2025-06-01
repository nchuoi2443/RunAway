using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossUIHandle : MonoBehaviour
{
    [SerializeField] private Slider _bossHealthSlider;
    private BossHealth _bossHealth;


    private void Update()
    {
        if (_bossHealth == null)
        {
            _bossHealth = FindObjectOfType<BossHealth>();
            if(_bossHealth == null) gameObject.SetActive(false);
            return;
        }
        
        _bossHealthSlider.maxValue = _bossHealth.MaxHealth;
        _bossHealthSlider.value = _bossHealth.CurrentHealth;
    }


}
