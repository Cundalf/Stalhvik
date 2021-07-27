using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(WeaponController))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(HealthManager))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 8f;

    Vector3 forward, right, rightMovement, upMovement;

    private Animator anim;
    private WeaponController weaponController;
    private HealthManager healthManager;
    private float inputH;
    private float inputV;

    public bool isMoving;

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        anim = GetComponent<Animator>();
        weaponController = GetComponent<WeaponController>();
        healthManager = GetComponent<HealthManager>();

    }

    void Update()
    {
        if(!weaponController.isAttacking)
        {
            inputH = Input.GetAxis("Horizontal");
            inputV = Input.GetAxis("Vertical");
        }

        if (inputH != 0.0f || inputV != 0.0f)
        {
            rightMovement = right * moveSpeed * Time.deltaTime * inputH;
            upMovement = forward * moveSpeed * Time.deltaTime * inputV;

            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

            transform.forward = heading;
            transform.position += rightMovement;
            transform.position += upMovement;
        }

        animatorControl();
        attackControl();
    }

    private void attackControl()
    {
        if (Input.GetButtonUp("Fire1") && !isMoving)
        {
            weaponController.attack();
            anim.SetTrigger("Attack");
        }
    }

    private void animatorControl()
    {
        if (inputH != 0.0f || inputV != 0.0f)
        {
            anim.SetBool("Run", true);
            isMoving = true;
        }
        else
        {
            anim.SetBool("Run", false);
            isMoving = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>();
            if (item == null)
                return;

            if(item.type == Item.ItemType.health)
            {
                healthManager.heal(item.amount);
            }

            if (item.type == Item.ItemType.soul)
            {
                GameManager.SharedInstance.addSoul(item.amount);
            }

            Destroy(other.gameObject);
        }
    }
}
