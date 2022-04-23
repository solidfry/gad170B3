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

    public void HasAcquired()
    {
        hasAcquired = true;
        this.gameObject.GetComponent<Renderer>().enabled = false;
        StartCoroutine(RespawnBoost());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Tank>() is not null)
            HasAcquired();
    }

    public IEnumerator RespawnBoost()
    {
        yield return new WaitForSeconds(boostCountDown);
        this.gameObject.GetComponent<Renderer>().enabled = true;
    }

}
