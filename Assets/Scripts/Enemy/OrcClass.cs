using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
public class OrcClass : MonoBehaviour
{
    public enum OrcClasses
    {
        berserker,
        warrior,
        paladin
    }

    [Header("Start config")]
    public List<GameObject> berserkerItems;
    public List<GameObject> warriorItems;
    public List<GameObject> paladinItems;

    public int berserkerHealth;
    public int warriorHealth;
    public int paladinHealth;

    public OrcClasses orcClass;

    [Header("In game config")]
    [SerializeField]
    private int berserkerDamage;
    [SerializeField]
    private int warriorDamage;
    [SerializeField]
    private int paladinDamage;

    public int Damage
    {
        get
        {
            switch (orcClass)
            {
                case OrcClasses.berserker:
                    return berserkerDamage;
                case OrcClasses.paladin:
                    return paladinDamage;
                case OrcClasses.warrior:
                    return warriorDamage;
            }

            return 0;
        }
    }


    void Start()
    {
        HealthManager hm = GetComponent<HealthManager>();

        switch(orcClass)
        {
            case OrcClasses.berserker:
                hm.changeMaxHealth(berserkerHealth);
                foreach(GameObject item in berserkerItems)
                {
                    item.SetActive(true);
                }
                break;
            case OrcClasses.paladin:
                hm.changeMaxHealth(paladinHealth);
                foreach (GameObject item in paladinItems)
                {
                    item.SetActive(true);
                }
                break;
            case OrcClasses.warrior:
                hm.changeMaxHealth(warriorHealth);
                foreach (GameObject item in warriorItems)
                {
                    item.SetActive(true);
                }
                break;
        }
    }
}
