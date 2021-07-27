using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : Tool
{
    protected override void OnTriggerStay(Collider other)
    {
        if (!canDamage)
            return;

        if (other.CompareTag("Enemy"))
        {
            damageEnemy(other.GetComponent<HealthManager>(), damage);
            return;
        }

        if (other.CompareTag("Resource"))
        {
            Resource r = other.GetComponent<Resource>();
            if (r.type == Resource.ResourceType.stone)
            {
                r.hit();
                playerInventory.addResource(Resource.ResourceType.stone);
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.CHOPPING_STONE);
                canDamage = false;
            }
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
