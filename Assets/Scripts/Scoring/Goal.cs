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
    [SerializeField]
    public Team team;
}
