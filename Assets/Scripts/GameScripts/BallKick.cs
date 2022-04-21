using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallKick : MonoBehaviour
{

    public float forceApplied = 300f;

    // is called when the trigger hits an object
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("It ran");
        if (other.gameObject.tag == "Ball")
        {
            other.rigidbody.AddExplosionForce(forceApplied, transform.position, 10f); ; //Kick the ball
        }

    }
}