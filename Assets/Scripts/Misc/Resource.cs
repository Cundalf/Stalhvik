using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public enum ResourceType
    {
        wood,
        stone,
        special
    }

    public ResourceType type;

    [SerializeField]
    private int hitsResist = 1;
    [SerializeField]
    private float respawnTime = 10.0f;

    private int health;
    private MeshRenderer mesh;
    private CapsuleCollider capColl;
    private BoxCollider boxColl;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        capColl = GetComponent<CapsuleCollider>();
        boxColl = GetComponent<BoxCollider>();

        health = hitsResist;
    }

    public void hit()
    {
        health--;

        if(health <= 0)
        {
            mesh.enabled = false;
            if(capColl != null)
                capColl.enabled = false;
            if (boxColl != null)
                boxColl.enabled = false;

            Invoke("respawn", respawnTime);
        }
    }

    private void respawn()
    {
        mesh.enabled = true;
        if (capColl != null)
            capColl.enabled = true;
        if (boxColl != null)
            boxColl.enabled = true;

        health = hitsResist;
    }
}
