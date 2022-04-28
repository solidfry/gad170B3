using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostManager : MonoBehaviour
{
    public List<Transform> boostSpawnLocations;

    public List<GameObject> boostsToSpawn;


    private void Awake()
    {
        SpawnBoosts();
    }
    private void OnDrawGizmos()
    {
        // loops through all the possible spawn points
        for (int i = 0; i < boostSpawnLocations.Count; i++)
        {
            Gizmos.color = Color.blue; // set the colour of our gizmo to blue
            Gizmos.DrawSphere(boostSpawnLocations[i].position, 1f); // draw a gizmo for our spawn point location
        }
    }

    ///<summary>
    /// Spawn in our boosts to each position. Choosing a random boost type each time.
    ///</summary>
    void SpawnBoosts()
    {
        foreach (Transform spawnPoint in boostSpawnLocations)
        {
            Transform spawnpoint = this.transform;
            var boost = Instantiate(boostsToSpawn[Random.Range(0, boostsToSpawn.Count)], spawnPoint);
            boost.transform.SetParent(this.gameObject.transform);
        }
    }

}
