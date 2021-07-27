using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public Vector3 reloadingPosition;
    public Vector3 readyPosition;
    public GameObject resource;
    public float speed;

    [SerializeField]
    private float cooldown = 250;
    [SerializeField]
    private int _amount = 5;

    public int amount
    {
        get
        {
            return _amount;
        }
    }

    private Vector3 target;

    void Start()
    {
        Invoke("ready", cooldown);
        target = transform.position;
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, target) >= 0.5f)
        {
            Vector3 dir = transform.forward;
            if (!resource.activeSelf)
            {
                dir = -transform.forward;
            }
            
            transform.position += dir * Time.deltaTime * speed;
        }
    }

    public void ready()
    {
        resource.SetActive(true);
        target = readyPosition;
    }

    public void goBack()
    {
        resource.SetActive(false);
        target = reloadingPosition;
        Invoke("ready", cooldown);
    }
}
