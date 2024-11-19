using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;

    [SerializeField] private GameObject particleOnHitPrefabVFX;

    private WeaponInfo weaponInfo;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }
    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    public void UpdateWeaponInfo(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = collision.gameObject.GetComponent<Indestructible>();

        if (!collision.isTrigger && (enemyHealth || indestructible))
        {
            enemyHealth?.TakeDamage(weaponInfo.weaponDamage);
            Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > weaponInfo.weaponRange)
        {
            Destroy(gameObject);
        }
    }
}
