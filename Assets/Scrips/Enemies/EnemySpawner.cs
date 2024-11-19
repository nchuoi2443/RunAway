using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private int wayCounter;
    private float wayTimerDefault;
    private float currentWayTimer;

    private TMP_Text wayText;
    private TMP_Text currentWayText;

    private void Awake()
    {
        wayCounter = 1;
        wayTimerDefault = 20f;
        currentWayTimer = wayTimerDefault;
        wayText = GameObject.Find("WayCounter").GetComponent<TMP_Text>();
        currentWayText = GameObject.Find("CurrentWay").GetComponent<TMP_Text>();

    }

    private void Update()
    {
        currentWayTimer -= Time.deltaTime;
        if (currentWayTimer <= 0)
        {
            currentWayTimer = wayTimerDefault;
            wayCounter++;
        }
        wayText.text = "Way:" + wayCounter.ToString();
        currentWayText.text = "Time left:" + currentWayTimer.ToString("F0");
    }
}
