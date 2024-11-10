using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    private void Update()
    {
        FaceMouse();
    }

    public void Attack()
    {
        Debug.Log("Bow Attack");
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }

    public void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerScreenMouse = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePosition.y - playerScreenMouse.y, mousePosition.x - playerScreenMouse.x) * Mathf.Rad2Deg;

        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);

    }
}
