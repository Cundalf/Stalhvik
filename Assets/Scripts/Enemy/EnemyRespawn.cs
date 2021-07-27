using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float timeToStart = 20f;
    public float respawnDelay = 5f;

    void Start()
    {
        InvokeRepeating("respawn", timeToStart, respawnDelay);
    }

    private void respawn()
    {
        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }
}
