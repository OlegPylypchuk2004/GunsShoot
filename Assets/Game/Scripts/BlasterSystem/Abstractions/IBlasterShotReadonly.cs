using System;

namespace BlasterSystem.Abstractions
{
    public interface IBlasterShotReadonly
    {
        public event Action ShotFired;
    }
}