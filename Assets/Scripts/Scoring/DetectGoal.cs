using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGoal : MonoBehaviour
{
    public Transform ballSpawnPoint;
    Transform currentPosition;

    private void Awake()
    {
        currentPosition = this.gameObject.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Goal>())
            GoalScored(other);
    }

    void GoalScored(Collider other)
    {
        TankGameEvents.OnGoalScoredEvent?.Invoke(other.gameObject.GetComponent<Collider>());
        Debug.Log("Goal scored event invoked");
        TankGameEvents.OnRoundResetEvent?.Invoke();
        TankGameEvents.SpawnTanksEvent?.Invoke(2);
        ResetBall(ballSpawnPoint);
    }

    public void ResetBall(Transform spawnLocation)
    {
        spawnLocation = ballSpawnPoint;
        currentPosition.position = ballSpawnPoint.position;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Debug.Log("Ball is reset");
    }
}
