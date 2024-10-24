using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHit : MonoBehaviour
{
    [SerializeField] private Material hitMaterial;
    [SerializeField] private float resetMaterialTime = 0.2f;
    private Material originalMaterial;
    private SpriteRenderer sprrend;
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        sprrend = GetComponent<SpriteRenderer>();
        originalMaterial = sprrend.material;
        enemyHealth = GetComponent<EnemyHealth>();
    }

    public IEnumerator GetHitEffect()
    {
        sprrend.material = hitMaterial;
        yield return new WaitForSeconds(resetMaterialTime);
        sprrend.material = originalMaterial;
    }
}
