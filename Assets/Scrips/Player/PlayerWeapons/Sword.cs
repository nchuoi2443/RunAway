﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private Transform slashPosSpawn;

    [SerializeField] private WeaponInfo weaponInfo;

    private Transform weaponCollider;
    private Animator animator;

    private GameObject slashAnim;
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        //swordCollider = GetComponentInParent<Collider2D>();
        slashPosSpawn = GameObject.Find("SlashSpawnPoint").transform;
    }

    private void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider;
    }



    public void Attack()
    {
        animator.SetTrigger("Slash");
        weaponCollider.gameObject.SetActive(true);

        slashAnim = Instantiate(slashPrefab, slashPosSpawn.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    public void doneAttackAnim()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    //flip anim swing up and swing down
    public void SwingUpFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Killed enemy");
        }
    }

    public void FollowingOffSet()
    {
        Vector3 mousePos = Input.mousePosition;
        //bug không lấy được instance của player controller, do scrip playerController nằm ở đối tượng parents 
        //nên getcombonent không khởi tạo được instance của playerController
        Vector3 playerScreenpoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenpoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            if (weaponCollider != null)
                weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            if (weaponCollider != null)
                weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void WeaponUpdate()
    {
        FollowingOffSet();
        if (slashPosSpawn == null)
        {
            slashPosSpawn = GameObject.Find("SlashSpawnPoint").transform;
        }
    }
}
