using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyStat : MonoBehaviour, IModifyStats
{
    public enum StatType { Speed }

    [SerializeField]
    public float boostValue = 5f;
    public float boostCountDown = 2f;
    public bool hasAcquired = false;

    /// <summary>
    /// If the boost has been acquired 
    /// </summary>
    public void HasAcquired()
    {
        hasAcquired = true;
        this.gameObject.GetComponent<Renderer>().enabled = false;
        this.gameObject.GetComponent<Collider>().enabled = false;
        StartCoroutine(RespawnBoost());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Tank>() != null && hasAcquired == false)
            HasAcquired();
    }

    /// <summary>
    /// After the boostCountDown time expires the boost is reactivated
    /// </summary>
    public IEnumerator RespawnBoost()
    {
        yield return new WaitForSeconds(boostCountDown);
        this.gameObject.GetComponent<Renderer>().enabled = true;
        this.gameObject.GetComponent<Collider>().enabled = true;
        hasAcquired = false;
    }

}
