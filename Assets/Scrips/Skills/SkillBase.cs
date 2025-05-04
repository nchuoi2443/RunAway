using System.Collections;
using UnityEngine;

[System.Serializable]
public abstract class SkillBase : ScriptableObject
{
    public string skillName;
    public string skillTriggerText;
    public float cooldown;
    protected float lastCastTime;

    public virtual bool CanCast()
    {
        return Time.time >= lastCastTime + cooldown;
    }

    public virtual void Cast(Animator animator)
    {
        lastCastTime = Time.time;
        animator.SetTrigger(skillTriggerText);
    }
}