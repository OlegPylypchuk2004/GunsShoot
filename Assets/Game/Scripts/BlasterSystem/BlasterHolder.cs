using System;
using UnityEngine;

namespace BlasterSystem
{
    public class BlasterHolder
    {
        public PhysicalProjectilesBlaster Blaster { get; private set; }
        public BlasterConfig[] Configs { get; private set; }

        public event Action<PhysicalProjectilesBlaster> BlasterChanged;

        public BlasterHolder()
        {
            Configs = Resources.LoadAll<BlasterConfig>("Configs/Blasters");
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

        public void ChangeBlasterRandom()
        {
            BlasterConfig blasterConfig = null;

            if (Configs.Length > 1 && Blaster != null)
            {
                do
                {
                    blasterConfig = Configs[UnityEngine.Random.Range(0, Configs.Length)];
                }
                while (blasterConfig == Blaster.Config);
            }
            else
            {
                blasterConfig = Configs[UnityEngine.Random.Range(0, Configs.Length)];
            }

            ChangeBlaster(blasterConfig);
        }
    }
}