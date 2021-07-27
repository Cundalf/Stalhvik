using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("Target to follow")]
    public Transform target;

    [SerializeField]
    [Tooltip("Velocity of camera")]
    private float velocity = 1.0f;
    [SerializeField]
    private Vector3 cameraOffset;

    void Update()
    {
        Vector3 newPos = target.position + cameraOffset;
        transform.position = Vector3.Lerp(transform.position, newPos, velocity * Time.deltaTime);
    }
}
