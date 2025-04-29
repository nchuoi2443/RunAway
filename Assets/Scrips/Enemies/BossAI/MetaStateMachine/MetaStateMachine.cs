using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MetaStateMachine : MonoBehaviour
{
    private IPhaseState _currentPhaseState;
    private BossBase _bossBase;

    private void Start()
    {
        ChangePhase(new NormalPhase(_bossBase, this));
    }

    public void ChangePhase(IPhaseState phaseState)
    {
        if (_currentPhaseState != null)
        {
            _currentPhaseState.ExitPhase();
        }
        _currentPhaseState = phaseState;
        _currentPhaseState.EnterPhase();
    }

    public void Update()
    {
        if (_currentPhaseState != null)
        {
            _currentPhaseState.UpdatePhase();
        }
    }
}
