using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyStat : MonoBehaviour, IModifyStats
{
    public enum StatType { Speed }

    [SerializeField]
    public float boostValue = 5f;
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
