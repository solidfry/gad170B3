using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGoal : MonoBehaviour
{
    public Transform ballSpawnPoint;
    Transform currentPosition;

    private void Awake()
    {
        // Get the current position of the ball
        currentPosition = this.gameObject.transform;
    }

    // 1.08 | Detect a goal when the ball enters the goal trigger area
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Goal>())
            GoalScored(other);
    }

    // When a goal is scored we invoke a bunch of events as well as reset the ball position
    void GoalScored(Collider other)
    {
        TankGameEvents.OnGoalScoredEvent?.Invoke(other.gameObject.GetComponent<Collider>());
        Debug.Log("Goal scored event invoked");
        //  1.09 | Events invoked 
        TankGameEvents.OnRoundResetEvent?.Invoke();
        TankGameEvents.SpawnTanksEvent?.Invoke(2);
        ResetBall(ballSpawnPoint);
    }

    // Ball is reset and velocity is set to zero.
    public void ResetBall(Transform spawnLocation)
    {
        spawnLocation = ballSpawnPoint;
        currentPosition.position = ballSpawnPoint.position;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Debug.Log("Ball is reset");
    }
}
