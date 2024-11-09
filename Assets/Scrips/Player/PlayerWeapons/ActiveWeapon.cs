using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour GetCurrentWeapon { get; private set; }

    private PlayerControls playerControls;
    private bool attackButtonPressed, isAttacking = false;
    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttack();
        playerControls.Combat.Attack.canceled += _ => StopAttack();

    }

    private void Update()
    {
        Attack();
    }

    public void ToggleIsAttacking(bool value)
    {
        isAttacking = value;
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
        if (attackButtonPressed && !isAttacking)
        {
            isAttacking = true;
            (GetCurrentWeapon as IWeapon).Attack();
        }
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        GetCurrentWeapon = newWeapon;
    }

    public void WeaponNull()
    {
        GetCurrentWeapon = null;
    }
}