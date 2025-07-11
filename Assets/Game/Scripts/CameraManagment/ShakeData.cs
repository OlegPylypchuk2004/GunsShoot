using DG.Tweening;
using System;

namespace CameraManagment
{
    [Serializable]
    public class ShakeData
    {
        public float Duration;
        public float Strength;
        public int Vibrations;
        public Ease Ease;
    }
}