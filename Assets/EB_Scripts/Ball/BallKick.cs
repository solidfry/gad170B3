using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallKick : MonoBehaviour
{

    public float forceApplied = 300f;

    // 1.08 | is called when the trigger hits an object and then adds a force, but only on a ball
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            other.rigidbody.AddExplosionForce(forceApplied, transform.position, 10f); ; //Kick the ball
        }

    }
}