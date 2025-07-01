using System;
using UnityEngine;

namespace BlasterSystem
{
    public class BlasterHolder
    {
        public Blaster Blaster { get; private set; }

        public event Action<Blaster> BlasterChanged;

        public BlasterHolder(BlasterConfig blasterConfig)
        {
            ChangeBlaster(blasterConfig);
        }

        public void ChangeBlaster(BlasterConfig blasterConfig)
        {
            if (Blaster != null)
            {
                GameObject.Destroy(Blaster.gameObject);
            }

            Blaster = GameObject.Instantiate(blasterConfig.Prefab);

            BlasterChanged?.Invoke(Blaster);
        }
    }
}