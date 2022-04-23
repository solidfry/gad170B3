using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGoal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Goal>())
            GoalScored(other);
    }

    void GoalScored(Collider other)
    {
        TankGameEvents.OnGoalScoredEvent?.Invoke(other.gameObject.GetComponent<Collider>());
        Debug.Log("Goal scored event invoked");
    }
}
