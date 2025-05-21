using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ShopManager _shopManager;
    [SerializeField] private TMP_Text _wayText;
    [SerializeField] private TMP_Text _currentWayText;
    
    private int _wayCounter;
    private float _wayTimerDefault;
    private float _currentWayTimer;
    private float _timeWaitBeforeNewWay = 4f;
    private bool _countable = true;

    private void Awake()
    {
        _wayCounter = 1;
        _wayTimerDefault = 30f;
        _currentWayTimer = _wayTimerDefault;
        _wayText = GameObject.Find("WayCounter").GetComponent<TMP_Text>();
        _currentWayText = GameObject.Find("CurrentWay").GetComponent<TMP_Text>();

    }

    private void Update()
    {
        if (LevelManager.Instance.IsPause) return;

        HandleTextOut();
        TimerHandle();
    }

    private void TimerHandle()
    {
        if (!_countable) return;

        _currentWayTimer -= Time.deltaTime;
        if (_currentWayTimer > 0) return;
        StopCoroutine(GameManager.Instance.SpawnEnemies());
        

        GameManager.Instance.RemoveAllEnemies();
        _shopManager.gameObject.SetActive(true);
        LevelManager.Instance.IsPause = true;
        Inventory.Instance.gameObject.SetActive(false);
        StartCoroutine(BeforeNewWay());

        _currentWayTimer = _wayTimerDefault;
        _wayCounter++;
        
    }

    private IEnumerator BeforeNewWay()
    {
        _countable = false;
        LevelManager.Instance.IsPause = false;
        yield return new WaitForSeconds(_timeWaitBeforeNewWay);
        if (_wayCounter % 2 == 0)
        {
            GameManager.Instance.SpawnBoss();
        }
        else
        {
            StartCoroutine(GameManager.Instance.SpawnEnemies());
            _countable = true;
        }
    }

    private void HandleTextOut()
    {
        _wayText.text = "Way:" + _wayCounter.ToString();
        _currentWayText.text = "Time left:" + _currentWayTimer.ToString("F0");
    }    
}
