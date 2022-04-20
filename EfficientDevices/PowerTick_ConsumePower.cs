using Assets.Scripts.Networks;
using Assets.Scripts.Objects.Electrical;
using Assets.Scripts.Objects.Pipes;
using HarmonyLib;
using Objects.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EfficientDevices
{
    [HarmonyPatch(typeof(PowerTick), "ConsumePower")]
    public class PowerTick_ConsumePower
    {
        static void Prefix(Device device, CableNetwork cableNetwork, ref float powerRequired)
        {
            if (device is TurboVolumePump)
            {
                if (powerRequired > Mod.TurboVolumePump_MaxPower.Value)
                {
                    powerRequired = Mod.TurboVolumePump_MaxPower.Value;
                }
            }

            if (device is AirConditioner)
            {
                if (powerRequired > Mod.AirConditioner_MaxPower.Value)
                {
                    powerRequired = Mod.AirConditioner_MaxPower.Value;
                }
            }
		}
    }
}
