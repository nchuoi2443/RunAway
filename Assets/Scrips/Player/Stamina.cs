using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// Singleton class definition
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

public class Stamina : Singleton<Stamina>
{
    public int currentStamina { get; private set; }

    [SerializeField] private int maxStamina = 3;
    [SerializeField] private Sprite fullStaminaSprite, emptyStamina;
    [SerializeField] private int staminaRefreashTime = 4;

    private Transform container;
    const string STAMINA_CONTEXT = "StaminaContainer";

    protected override void Awake()
    {
        base.Awake();

        currentStamina = maxStamina;
        container = GameObject.Find(STAMINA_CONTEXT).transform;
        if (container == null)
        {
            Debug.LogError("Stamina container not found");
        }
        StartCoroutine(RefreashStaminaCoroutine());
    }

    public void UseStamina()
    {
        currentStamina--;
        UpdateStaminaUI();
        //StartCoroutine(RefreashStaminaCoroutine());
    }

    public void RefreashStamina()
    {
        if (currentStamina >= maxStamina)
        {
            return;
        }

        currentStamina++;
        UpdateStaminaUI();
    }

    public IEnumerator RefreashStaminaCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(staminaRefreashTime);
            RefreashStamina();
        }
    }

    public void UpdateStaminaUI()
    {
        for (int i = 0; i < maxStamina; i++)
        {
            if (i < currentStamina)
            {
                container.GetChild(i).GetComponent<Image>().sprite = fullStaminaSprite;
            }
            else
            {
                container.GetChild(i).GetComponent<Image>().sprite = emptyStamina;
            }
        }
    }

}
