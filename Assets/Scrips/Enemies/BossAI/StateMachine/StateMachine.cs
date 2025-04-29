using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public static StateMachine Instance;
    private IState _currentState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeState(IState state)
    {
        if (_currentState != null)
        {
            _currentState.ExitState();
        }
        _currentState = state;
        _currentState.EnterState();
    }

    public void StateMachineUpdate()
    {
        _currentState.UpdateState();
    }
}
