using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifySpeed : MonoBehaviour, IModifyStats
{
    [SerializeField]
    public float speedBoost = 5f;
    public bool hasAcquired = false;

    public void HasAcquired()
    {
        hasAcquired = true;
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Tank>() is not null)
            HasAcquired();
    }

}
