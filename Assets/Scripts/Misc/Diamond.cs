using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    /// <summary>
    /// Perform Diamond Rotation (Enemies Target)
    /// </summary>    

    public Transform diamond;
    public float diamondRotationSpeed;

    void Update()
    {
        diamond.Rotate(new Vector3(0f, 1f, 0f) * Time.deltaTime * diamondRotationSpeed);
    }
}
