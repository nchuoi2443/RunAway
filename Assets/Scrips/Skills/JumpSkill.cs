using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpSkill", menuName = "Skills/JumpSkill")]
public class JumpSkill : BaseSkill
{
    
    public float DelayBeforeJump = 0.5f;
    public float JumpDuration = 0.5f;

    public override void ActivateSkill(Transform caster, Transform target)
    {
        Vector3 targetPos = target.position;
        caster.GetComponent<MonoBehaviour>().StartCoroutine(JumpRoutine(caster, targetPos));
    }

    private IEnumerator JumpRoutine(Transform caster, Vector3 targetPos)
    {
        Animator anim = caster.GetComponent<Animator>();

        yield return new WaitForSeconds(DelayBeforeJump);
        Vector3 startPos = caster.position;
        float elapsed = 0f;

        while (elapsed < JumpDuration)
        {
            caster.position = Vector3.Lerp(startPos, targetPos, elapsed / JumpDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        caster.position = targetPos;
    }
}
