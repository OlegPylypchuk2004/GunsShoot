using System;
using UnityEngine;

namespace BlasterSystem
{
    public class BlasterHolder
    {
        public Blaster Blaster { get; private set; }
        public BlasterConfig[] Configs { get; private set; }

        public event Action<Blaster> BlasterChanged;

        public BlasterHolder()
        {
            Configs = Resources.LoadAll<BlasterConfig>("Configs/Blasters");
            ChangeBlaster(Configs[UnityEngine.Random.Range(0, Configs.Length)]);
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