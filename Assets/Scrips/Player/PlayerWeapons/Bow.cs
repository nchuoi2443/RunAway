using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private Transform arrowSpawnPoint;

    private int FIRE_HASH = Animator.StringToHash("Fire");

    private Animator animator;
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        FaceMouse();
    }

    public void Attack()
    {
        animator.SetTrigger(FIRE_HASH);
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        newArrow.GetComponent<ProjectTile>().UpdateWeaponInfo(weaponInfo);
    }

    public void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerScreenMouse = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePosition.y - playerScreenMouse.y, mousePosition.x - playerScreenMouse.x) * Mathf.Rad2Deg;

        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);

    }

    
}
