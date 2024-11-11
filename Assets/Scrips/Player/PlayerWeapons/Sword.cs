using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private Transform slashPosSpawn;
    [SerializeField] private float swordAttackCD = 0.5f;

    private Transform weaponCollider;
    private Animator animator;


    private GameObject slashAnim;
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

    private void Update()
    {
        followingOffSet();
        if (slashPosSpawn == null)
        {
            slashPosSpawn = GameObject.Find("SlashSpawnPoint").transform;
        }
    }


    public void Attack()
    {
        Debug.Log("Sword attack");
        animator.SetTrigger("Slash");
        weaponCollider.gameObject.SetActive(true);

        slashAnim = Instantiate(slashPrefab, slashPosSpawn.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
        StartCoroutine(AttackCDCoroutine());
        /*if (ActiveWeapon.Instance.IsAttacking)
        {
            ActiveWeapon.Instance.ToggleIsAttacking(false);
        }*/
    }

    private IEnumerator AttackCDCoroutine()
    {
        yield return new WaitForSeconds(swordAttackCD);
        //bug here, sau khi đổi vũ khí, auto cái isAttacking = true? wtf
        ActiveWeapon.Instance.ToggleIsAttacking(false);
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

    private void followingOffSet()
    {
        Vector3 mousePos = Input.mousePosition;
        //bug không lấy được instance của player controller, do scrip playerController nằm ở đối tượng parents 
        //nên getcombonent không khởi tạo được instance của playerController
        Vector3 playerScreenpoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenpoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Killed enemy");
        }
    }

    
}
