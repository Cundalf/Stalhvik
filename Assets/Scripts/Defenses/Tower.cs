using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject lightning;
    public GameObject lightningTargetPoint;
    public float detectionRange;

    [SerializeField]
    private float attackCooldown = 1f;
    [SerializeField]
    private int damage = 1;

    private float attackCounter = 0f;
    private AudioSource audioSource;
    private Transform target;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("findEnemy", 0.25f, 0.25f);
    }

    void Update()
    {
        if (target == null)
        {
            lightning.SetActive(false);
            return;
        }

        if (Vector3.Distance(transform.position, target.position) > detectionRange)
        {
            resetTarget();
            return;
        }

        RaycastHit[] rays = Physics.RaycastAll(transform.position, target.position - transform.position, detectionRange);

        if (rays.Length == 0)
        {
            resetTarget();
            return;
        }

        if (!rays[0].collider.CompareTag("Enemy"))
        {
            resetTarget();
            return;
        }

        lightningTargetPoint.transform.position = target.position;
        attackCounter += Time.deltaTime;
        if(attackCounter>= attackCooldown)
        {
            audioSource.Play();
            target.gameObject.GetComponent<HealthManager>().hit(damage);
            attackCounter = 0f;
        }
    }

    private void resetTarget()
    {
        attackCounter = 0f;
        target = null;
        lightning.SetActive(false);
    }

    private void findEnemy()
    {
        if (target != null)
            return;

        int mask = (1 << LayerMask.NameToLayer("Enemy"));
        Collider[] cols = Physics.OverlapSphere(transform.position, detectionRange, mask);

        if (cols.Length == 0)
            return;

        foreach (Collider col in cols)
        {
            RaycastHit[] rays = Physics.RaycastAll(transform.position, col.gameObject.transform.position - transform.position, detectionRange);
            if (rays.Length == 0)
                continue;

            if (rays[0].collider.CompareTag("Enemy"))
            {
                target = col.gameObject.transform;
                lightning.SetActive(true);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
