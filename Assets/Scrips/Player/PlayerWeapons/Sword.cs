using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private Transform slashPosSpawn;

    private Animator animator;
    private PlayerControls playerControls;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private GameObject slashAnim;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }
    private void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update()
    {
        followingOffSet();
    }
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
        }
        playerControls.Enable();
    }

   
    private void Attack()
    {
        animator.SetTrigger("Slash");

        slashAnim = Instantiate(slashPrefab, slashPosSpawn.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    public void SwingUpFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void followingOffSet()
    {
        Vector3 mousePos = Input.mousePosition;
        //bug không lấy được instance của player controller, do scrip playerController nằm ở đối tượng parents 
        //nên getcombonent không khởi tạo được instance của playerController
        Vector3 playerScreenpoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenpoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
