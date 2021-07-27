using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcWeapon : MonoBehaviour
{
    public OrcClass orcClass;
    public EnemyController enemy;

    void OnTriggerStay(Collider other)
    {
        if (!enemy.canDamage)
            return;

        if (other.CompareTag("Player") || other.CompareTag("Defense") || other.CompareTag("Objetive"))
        {
            if (other.GetComponent<HealthManager>().hit(orcClass.Damage))
            {
                enemy.targetEliminate();
            }
            enemy.canDamage = false;
        }
    }

}
