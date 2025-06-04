using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    [SerializeField] private ShopManager _shopManager;
    [SerializeField] private TMP_Text _wayText;
    [SerializeField] private TMP_Text _currentWayText;
    [SerializeField] private BossUIHandle _bossUIHandle;

    private int _wayCounter;
    private float _wayTimerDefault;
    private float _currentWayTimer;
    private float _timeWaitBeforeNewWay = 4f;
    private bool _countable = true;
    private Coroutine _beforeWaveCourotine;
    private void Awake()
    {
        Instance = this;

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
        GameManager.Instance.StopSpawning();

        if (_beforeWaveCourotine != null)
        {
            StopCoroutine(_beforeWaveCourotine);
        }

        GameManager.Instance.RemoveAllEnemies();
        _shopManager.gameObject.SetActive(true);
        LevelManager.Instance.IsPause = true;
        Inventory.Instance.gameObject.SetActive(false);
        _beforeWaveCourotine = StartCoroutine(BeforeNewWay());

        _currentWayTimer = _wayTimerDefault;
        _wayCounter++;
        
    }

    public void UnCountable()
    {
        _countable = false;
    }

    private IEnumerator BeforeNewWay()
    {
        _countable = false;
        LevelManager.Instance.IsPause = false;

        while (ShopManager.Instance.gameObject.activeSelf)
        {
            yield return null;
        }

        yield return new WaitForSeconds(_timeWaitBeforeNewWay);
        if (_wayCounter % 2 == 0)
        {
            _bossUIHandle.gameObject.SetActive(true);
            GameManager.Instance.SpawnBoss();
            GameManager.Instance.StartSpawning();
        }
        else
        {
            _bossUIHandle.gameObject.SetActive(false);
            GameManager.Instance.StartSpawning();
            _countable = true;
        }
    }

    public void IsEndBossWay()
    {
        _countable = true;
        GameManager.Instance.StopSpawning();
        GameManager.Instance.RemoveAllEnemies();
        _wayCounter++;
        StartCoroutine(WaintToOpenShop());
    }

    IEnumerator WaintToOpenShop()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.StopSpawning();
        _shopManager.gameObject.SetActive(true);
        LevelManager.Instance.IsPause = true;
        Inventory.Instance.gameObject.SetActive(false);
        if (_beforeWaveCourotine != null)
        {
            StopCoroutine(_beforeWaveCourotine);
        }
        _beforeWaveCourotine = StartCoroutine(BeforeNewWay());
    }

    private void HandleTextOut()
    {
        _wayText.text = "Way:" + _wayCounter.ToString();
        _currentWayText.text = "Time left:" + _currentWayTimer.ToString("F0");
    }    
}
