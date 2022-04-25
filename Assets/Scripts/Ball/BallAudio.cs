using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAudio : MonoBehaviour
{
    public AudioSource ballAudioSource;
    public AudioClip ballBounce;
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.GetComponent<Goal>())
        {
            ballAudioSource.PlayOneShot(ballBounce);
        }
    }
}
