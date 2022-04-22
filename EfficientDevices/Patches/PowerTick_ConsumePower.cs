using Assets.Scripts.Networks;
using Assets.Scripts.Objects.Electrical;
using Assets.Scripts.Objects.Pipes;
using Core;
using Core.Shared;
using HarmonyLib;
using Objects.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EfficientDevices.Patches
{
    [HarmonyPatch(typeof(PowerTick), "ConsumePower")]
    public class PowerTick_ConsumePower
    {
        /// <summary>
        /// Patches PowerTick.ConsumePower to change the powerRequired parameter without modifying the original method (it causes bugs)
        /// </summary>
        /// <param name="device"></param>
        /// <param name="cableNetwork"></param>
        /// <param name="powerRequired"></param>
        static void Prefix(Device device, CableNetwork cableNetwork, ref float powerRequired)
        {
            if (device is TurboVolumePump)
            {
                Utils.AssignConfigValue(ref powerRequired, Mod.TurboVolumePump_Config);
            }

            if (device is AirConditioner)
            {
                Utils.AssignConfigValue(ref powerRequired, Mod.AirConditioner_Config);
            }
		}
    }
}
