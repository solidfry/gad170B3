using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script rotates stuff
public class Rotate : MonoBehaviour
{

    public float rotateSpeed = 5f;
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * rotateSpeed);
    }
}
