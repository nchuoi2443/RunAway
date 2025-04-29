using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPhaseState
{
    public void EnterPhase();
    public void UpdatePhase();
    public void ExitPhase();
}
