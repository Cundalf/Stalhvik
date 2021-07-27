using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Header("Dependencies")]
    public GameObject defensePrefab;
    public Vector3 groundOffset;
    public Vector3 colliderOffset;
    public Vector3 colliderSize;

    [Header("Cost")]
    public int woodCost;
    public int stoneCost;
    public int specialCost;

    public bool Build()
    {
        int mask = ~(1 << LayerMask.NameToLayer("Player"));
        Collider[] hitColliders = Physics.OverlapBox(transform.position + colliderOffset, colliderSize,transform.rotation, mask);
        if (hitColliders.Length > 0)
        {
            Debug.Log("No se puede construir aca");
            return false;
        }

        mask = (1 << LayerMask.NameToLayer("Ground"));
        RaycastHit[] rayHits = Physics.RaycastAll(transform.position, new Vector3(0f, -1f, 0f), 10f, mask);
        if (rayHits.Length == 0)
        {
            Debug.Log("No hay un suelo construible");
            return false;
        }


        Vector3 pos = new Vector3(transform.position.x, rayHits[0].transform.position.y, transform.position.z) + groundOffset;

        mask = (1 << LayerMask.NameToLayer("Obstacle"));
        hitColliders = Physics.OverlapBox(pos + colliderOffset, colliderSize, transform.rotation, mask);

        foreach(Collider col in hitColliders)
        {
            Destroy(col.gameObject);
        }

        Instantiate(defensePrefab, pos, transform.rotation);
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.CRAFT);

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + colliderOffset, colliderSize);
    }
}
