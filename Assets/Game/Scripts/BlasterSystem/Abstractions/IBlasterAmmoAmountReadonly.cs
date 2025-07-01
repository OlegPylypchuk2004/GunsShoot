using System;

namespace BlasterSystem.Abstractions
{
    public interface IBlasterAmmoAmountReadonly
    {
        public int AmmoAmount { get; }
        public int MaxAmmoAmount { get; }

        public event Action<int> AmmoAmountChanged;
    }
}