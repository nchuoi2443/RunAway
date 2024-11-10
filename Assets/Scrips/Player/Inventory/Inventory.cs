using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int activeItemIndex = 0;
   
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
        ToggleActiveHighlight(0);
    }

    private void Start()
    {
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }

    private void OnEnable()
    {
        playerControls.Inventory.Enable();
    }

    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighlight(numValue - 1);
    }

    private void ToggleActiveHighlight(int indexNum)
    {
        activeItemIndex = indexNum;
        foreach (Transform child in this.transform)
        {
            child.GetChild(0).gameObject.SetActive(false);
        }
        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeInventoryItem();
    }

    private void ChangeInventoryItem()
    {
        Debug.Log(transform.GetChild(activeItemIndex).GetComponent<InventorySlot>().GetWeaponInfo().weaponPrefab.name);
        if (ActiveWeapon.Instance.GetCurrentWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.GetCurrentWeapon.gameObject);
        }

        if (!transform.GetChild(activeItemIndex).GetComponentInChildren<InventorySlot>())
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject weaponToSpawn = transform.GetChild(activeItemIndex).
            GetComponentInChildren<InventorySlot>().GetWeaponInfo().weaponPrefab;
        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,0, 0);
        newWeapon.transform.parent = ActiveWeapon.Instance.transform;
        Debug.Log("New Weapon: " + newWeapon.name);
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>()); 
    }
}
