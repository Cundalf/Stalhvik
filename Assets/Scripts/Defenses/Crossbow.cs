using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Crossbow : MonoBehaviour
{
    public GameObject arrowGO;
    public GameObject respawnGO;
    public GameObject crossbowGO;
    public float detectionRange;
    private Animator _anim;
    private Transform target;
    private AudioSource audioSource;

    [SerializeField]
    private float timeReload = 2.5f;
    private bool canShoot = true;


    void Start()
    {
        _anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("findEnemy", 0.25f, 0.25f);
    }

    void Update()
    {

        if (target == null)
            return;

        if (inRange(target))
        {
            crossbowGO.transform.LookAt(new Vector3(target.position.x, crossbowGO.transform.position.y, target.position.z), respawnGO.transform.up);
            crossbowGO.transform.Rotate(0, -90, 0);

            if (canShoot)
            {
                Instantiate(arrowGO, respawnGO.transform.position, respawnGO.transform.rotation);
                canShoot = false;
                Invoke("reload", timeReload);
                audioSource.Play();
            }
        }
        else
        {
            target = null;
        }
    }

    private void reload()
    {
        _anim.SetTrigger("reload");
    }

    public void reloadFinished()
    {
        canShoot = true;
    }

    private bool inRange(Transform posTarget)
    {
        Vector3 directionOfEnemy = transform.position - posTarget.position;
        float angle = Vector3.Angle(transform.forward, directionOfEnemy);

        if ((Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270) && Vector3.Distance(transform.position, posTarget.position) < detectionRange)
        {
            Debug.DrawLine(respawnGO.transform.position, posTarget.position, Color.red);
            return true;
        }

        return false;
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
                if (inRange(rays[0].collider.gameObject.transform))
                    target = col.gameObject.transform;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
