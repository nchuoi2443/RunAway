using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Observer : MonoBehaviour
{
    [SerializeField] private UnityEvent unityEvent;

    public void AddListener(UnityAction unityAction)
    {
        unityEvent.AddListener(unityAction);
    }

    public void RemoveListener(UnityAction unityAction)
    {
        unityEvent.RemoveListener(unityAction);
    }

    public void Notify()
    {
        unityEvent?.Invoke();
    }
}
