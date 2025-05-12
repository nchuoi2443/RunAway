using UnityEngine;

[System.Serializable]
public abstract class BaseSkill : ScriptableObject
{
    public string skillName;
    public float cooldownTime;
    [HideInInspector] public float currentCooldown = 0;

    public bool IsReady()
    {
        return currentCooldown <= 0f;
    }

    public void ReduceCooldown(float deltaTime)
    {
        if (currentCooldown > 0f)
            currentCooldown -= deltaTime;
    }

    public void StartCooldown()
    {
        currentCooldown = cooldownTime;
    }

    public abstract void ActivateSkill(Transform caster, Transform target);
}
