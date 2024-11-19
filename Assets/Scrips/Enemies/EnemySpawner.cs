using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private int wayCounter;
    private float wayTimerDefault;
    private float currentWayTimer;

    private float timeWaitBeforeNewWay = 4f;

    private TMP_Text wayText;
    private TMP_Text currentWayText;

    private bool countable = true;
    private void Awake()
    {
        wayCounter = 1;
        wayTimerDefault = 30f;
        currentWayTimer = wayTimerDefault;
        wayText = GameObject.Find("WayCounter").GetComponent<TMP_Text>();
        currentWayText = GameObject.Find("CurrentWay").GetComponent<TMP_Text>();

    }

    private void Update()
    {
        
        HandleTextOut();

        TimerHandle();
    }

    private void TimerHandle()
    {
        if (!countable) return;

        currentWayTimer -= Time.deltaTime;
        if (currentWayTimer > 0) return;
        StopCoroutine(GameManager.Instance.SpawnEnemies());
        

        GameManager.Instance.RemoveAllEnemies();
        StartCoroutine(BeforeNewWay());

        currentWayTimer = wayTimerDefault;
        wayCounter++;
        
    }

    private IEnumerator BeforeNewWay()
    {
        countable = false;
        yield return new WaitForSeconds(timeWaitBeforeNewWay);
        StartCoroutine(GameManager.Instance.SpawnEnemies());
        countable = true;
    }

    private void HandleTextOut()
    {
        wayText.text = "Way:" + wayCounter.ToString();
        currentWayText.text = "Time left:" + currentWayTimer.ToString("F0");
    }    
}
