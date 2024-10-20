using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float knockBackTime = .2f;

    private Rigidbody2D rb;
    public bool isKnockBack { get; private set; }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //create a method that will be called when enemy is hit and get a direction of knockback with knockback time
    // Method to apply knockback
    public void GetKnockBack(Transform damageSource, float knockBackThrush)
    {
        isKnockBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrush * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockBackTimer());
    }
    // Coroutine to reset knockback
    private IEnumerator KnockBackTimer()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        isKnockBack = false;
    }


}
