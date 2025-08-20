using System;
using UnityEngine;

[Serializable]
public class StageData
{
    [Min(0f)] public float ObstacleLaunchForceMultiplier;
    [Min(0f)] public float ObstacleGravityMultiplier;
    [Min(1)] public int MinObstaclesCount;
    [Min(1)] public int MaxObstaclesCount;
}