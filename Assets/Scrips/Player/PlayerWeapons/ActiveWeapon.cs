using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour GetCurrentWeapon { get; private set; }

    private PlayerControls playerControls;
    private bool attackButtonPressed, isAttacking = false;
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
        Debug.Log(isAttacking);
        Debug.Log("attackbuttonpressed: " +  attackButtonPressed);
        if (attackButtonPressed && !isAttacking && !Inventory.Instance.IsInventoryNull)
        {
            isAttacking = true;
            (GetCurrentWeapon as IWeapon).Attack();
            Debug.Log("is attacking");
        }
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        GetCurrentWeapon = newWeapon;

        isAttacking = false;
    }

    public void WeaponNull()
    {
        GetCurrentWeapon = null;
        isAttacking = false;
    }
}
