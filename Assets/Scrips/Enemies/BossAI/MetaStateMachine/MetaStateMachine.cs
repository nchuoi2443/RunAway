using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MetaStateMachine : MonoBehaviour
{
    public IPhaseState CurrentPhaseState;
    [SerializeField] private BossBase _bossBase;

    private void Start()
    {
        ChangePhase(new NormalPhase(_bossBase, this));
    }

    public void ChangePhase(IPhaseState phaseState)
    {
        if (CurrentPhaseState != null)
        {
            CurrentPhaseState.ExitPhase();
        }
        CurrentPhaseState = phaseState;
        CurrentPhaseState.EnterPhase();
    }

    public void Update()
    {
        if (CurrentPhaseState != null)
        {
            CurrentPhaseState.UpdatePhase();
            _bossBase.BossUpdate();
        }
    }
}
