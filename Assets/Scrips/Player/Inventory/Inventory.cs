﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    private int activeItemIndex = 0;
   
    private PlayerControls playerControls;
    public bool IsInventoryNull { set; get; }
    protected void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
            playerControls = new PlayerControls();
        ToggleActiveHighlight(0);
    }

    private void Start()
    {

        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighlight(numValue - 1);
    }

    private void ToggleActiveHighlight(int indexNum)
    {
        if (transform == null) return;

        activeItemIndex = indexNum;
        foreach (Transform child in transform)
        {
            child.GetChild(0).gameObject.SetActive(false);
        }
        transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeInventoryItem();
    }

    private void ChangeInventoryItem()
    {
        //Debug.Log(transform.GetChild(activeItemIndex).GetComponent<InventorySlot>().GetWeaponInfo().weaponPrefab.name);
        if (ActiveWeapon.Instance.GetCurrentWeapon != null)
        {
            // Cast the IWeapon to MonoBehaviour to access the gameObject property
            MonoBehaviour currentWeaponMono = ActiveWeapon.Instance.GetCurrentWeapon as MonoBehaviour;
            if (currentWeaponMono != null)
            {
                Destroy(currentWeaponMono.gameObject);
            }
            playerControls.Combat.Enable();
        }

        if (!transform.GetChild(activeItemIndex).GetComponentInChildren<InventorySlot>())
        {
            ActiveWeapon.Instance.WeaponNull();
            IsInventoryNull = true;
            return;
        }

        GameObject weaponToSpawn = transform.GetChild(activeItemIndex).
            GetComponentInChildren<InventorySlot>().GetWeaponInfo().weaponPrefab;

        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);

        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,0, 0);

        newWeapon.transform.parent = ActiveWeapon.Instance.transform;

        IsInventoryNull = false;
        Debug.Log("New Weapon: " + newWeapon.name);
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<IWeapon>());
    }
}
