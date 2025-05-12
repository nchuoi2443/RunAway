using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{

    [SerializeField] private List<SkillData> skillDataList;

    private Dictionary<BossSkillType, BaseSkill> skillDict = new();

    private void Awake()
    {
        //Instance = this;
        foreach (var data in skillDataList)
        {
            if (!skillDict.ContainsKey(data.skillType))
            {
                skillDict[data.skillType] = data.skill;
            }
        }
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        foreach (var skill in skillDict.Values)
        {
            skill.ReduceCooldown(delta);
        }
    }

    public bool TryCastSkill(BossSkillType type, Transform caster, Transform target)
    {
        if (skillDict.TryGetValue(type, out var skill) && skill.IsReady())
        {
            skill.ActivateSkill(caster, target);
            skill.StartCooldown();
            return true;
        }
        return false;
    }

    public bool IsSkillReady(BossSkillType type)
    {
        return skillDict.TryGetValue(type, out var skill) && skill.IsReady();
    }
}



public enum BossSkillType
{
    Melee,
    Jump,
    Magic,
    SpitFire,
}

[System.Serializable]
public class SkillData
{
    public BossSkillType skillType;
    public BaseSkill skill;
}