using System;
using UnityEngine;

namespace ComboSystem
{
    [Serializable]
    public class ComboStageData
    {
        [Min(0f)] public float ScoreMultiplier;
        [Min(0f)] public float Time;
    }
}