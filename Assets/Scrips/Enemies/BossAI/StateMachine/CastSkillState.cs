using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSkillState : IState
{
    public static CastSkillState Instance { get; private set; }
    public SkillManager SkillManager;
    private BossBase _bossBase;
    private Animator _animator;
    private string _skillName;
    private StateMachine _stateMachine;
    private int _skillHash;
    private bool _hasFinished;
    private bool _isLoop;
    private int _loopCount = 0;
    private int _targetLoop = 3;

    private Dictionary<int, string> _stateNameByHash = new Dictionary<int, string>();

    public CastSkillState(BossBase bossBase, string skillName, StateMachine stateMachine)
    {
        _bossBase = bossBase;
        _skillName = skillName;
        _stateMachine = stateMachine;
        if (_skillName == ActionState.magicSkill.ToString())
            _isLoop = true;
        else
            _isLoop = false;
    }

    public void EnterState()
    {
        _animator = _bossBase.GetComponent<Animator>();
        SkillManager = _bossBase.SkillManager;
        _hasFinished = false;
        _skillHash = Animator.StringToHash("Base Layer.CastSkill." + GetSkillName(_skillName));
        _animator.SetTrigger(_skillName);
        Instance = this;
    }

    public void UpdateState()
    {
        var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (!_animator.IsInTransition(0) && stateInfo.fullPathHash == _skillHash)
        {
            if (_isLoop)
            {
                if (stateInfo.normalizedTime >= _loopCount + .8)
                {
                    _loopCount++;

                    if (_loopCount >= _targetLoop)
                    {
                        _stateMachine.ChangeState(new IdleState(_bossBase, _stateMachine));
                    }
                }
            }
            else
            {
                if (!_hasFinished && stateInfo.normalizedTime >= .75f)
                {
                    _hasFinished = true;
                    _stateMachine.ChangeState(new IdleState(_bossBase, _stateMachine));
                }
            }
        }
    }

    public void ExitState()
    {
        _skillName = null;
        _bossBase = null;
        _animator = null;
        SkillManager = null;
        Instance = null;
    }
    
    private string GetSkillName(string skillName)
    {
        switch (skillName)
        {
            case nameof(ActionState.knifeSkill):
                return "KnifeSkill";
            case nameof(ActionState.magicSkill):
                return "CastMagicSkill";
            case nameof(ActionState.jumpSkill):
                return "JumpSkill";
            case nameof(ActionState.spitFireSkill):
                return "SpitFireSkill";
            default:
                return "UnknownSkill";
        }
    }

    
}
