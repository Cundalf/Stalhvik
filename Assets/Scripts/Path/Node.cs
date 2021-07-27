using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    float range = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public Vector3 getPoint()
    {
        Vector3 point = transform.position + Random.insideUnitSphere.normalized * range;
        point.y = transform.position.y;
        return point;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
