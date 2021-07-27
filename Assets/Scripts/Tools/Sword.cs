using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Tool
{
    [SerializeField]
    [Range(0f, 1f)]
    private float criticalChance;

    [SerializeField]
    [Min(1f)]
    private float criticalRate;

    protected override void OnTriggerStay(Collider other) {
        if (!canDamage)
            return;

        if (other.CompareTag("Enemy"))
        {
            float randValue = Random.value;

            float totalDamage = damage;
            if (randValue < (1f - criticalChance))
            {
                totalDamage *= criticalRate;
            }

            damageEnemy(other.GetComponent<HealthManager>(), (int)totalDamage);
        }

        if (other.CompareTag("Train"))
        {
            Train train = other.GetComponent<Train>();
            playerInventory.addResource(Resource.ResourceType.special, train.amount);
            train.goBack();
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RUBY);
            canDamage = false;
        }
    }
}
