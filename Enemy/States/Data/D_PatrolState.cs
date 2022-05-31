using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "newPatrolStateData", menuName = "Data/State Data/Patrol State")]
public class D_PatrolState : ScriptableObject
{
    public List<Vector2> pathCoordinates = new List<Vector2>();
    public float patrolSpeed;
}
