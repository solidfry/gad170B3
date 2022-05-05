using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Team
{
    Red,
    Blue,
    None
};
public class Goal : MonoBehaviour
{
    // We need to set the owner of the goal as a team
    [SerializeField]
    public Team team;
}
