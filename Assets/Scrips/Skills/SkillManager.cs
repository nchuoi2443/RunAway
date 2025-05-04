using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SkillManager : MonoBehaviour
{
    public List<SkillBase> skills;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TryCastSkill(int index)
    {
        if (index < 0 || index >= skills.Count) return;

        var skill = skills[index];
        if (skill.CanCast())
        {
            skill.Cast(animator);
        }
    }

    public SkillBase GetSkillByName(string name)
    {
        return skills.Find(skill => skill.skillName == name);
    }
}
