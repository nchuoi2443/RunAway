using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour GetCurrentWeapon { get; private set; }

    private PlayerControls playerControls;
    private bool attackButtonPressed, isAttacking = false;

    private float timeBetweenattacks;

    //getter for isAttacking
    public bool IsAttacking { get => isAttacking; }
    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
        transform.parent = PlayerController.Instance.transform;
    }

    private void OnEnable()
    {
        playerControls.Combat.Enable();
        playerControls.Combat.Attack.started += _ => StartAttack();
        playerControls.Combat.Attack.canceled += _ => StopAttack();

        
    }

    private void Start()
    {
        
        // set pos cho weapon cho player
        transform.position = PlayerController.Instance.transform.position;
        AttackCooldown();
    }

    private void Update()
    {
        Attack();
       
    }

    private void AttackCooldown()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(AttackCooldownRoutine());
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(timeBetweenattacks);
        isAttacking = false;
    }

    private void StartAttack()
    {
        attackButtonPressed = true;
    }

    private void StopAttack()
    {
        attackButtonPressed = false;
    }

    private void Attack()
    {
        if (attackButtonPressed && !isAttacking && !Inventory.Instance.IsInventoryNull)
        {
            AttackCooldown();
            (GetCurrentWeapon as IWeapon).Attack();
        }
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        GetCurrentWeapon = newWeapon;

        isAttacking = false;
        AttackCooldown();
        timeBetweenattacks = (GetCurrentWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }

    public void WeaponNull()
    {
        GetCurrentWeapon = null;
        isAttacking = false;
    }
}
