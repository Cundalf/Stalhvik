using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public Vector3 offset;
    public GameObject lootPrefab;
    [Range(0f, 1f)]
    public float probability;

    public bool loot()
    {
        if(Random.value > 1 - probability)
        {
            Instantiate(lootPrefab, transform.position + offset, transform.rotation);
            return true;
        }

        return false;
    }
}
