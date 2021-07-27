using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Tool : MonoBehaviour
{
    public WeaponController.ToolType type;
    public bool canDamage;

    protected Inventory playerInventory;

    [SerializeField]
    protected int damage;
    [SerializeField]
    protected float attackDelay;


    private void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        canDamage = false;
    }

    protected virtual void OnTriggerStay(Collider other) { }

    protected void damageEnemy(HealthManager health, int amount)
    {
        canDamage = false;

        if (amount < 0)
            return;
        health.hit(amount);
    }
}
