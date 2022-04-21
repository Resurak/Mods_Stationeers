using Assets.Scripts.Networks;
using Assets.Scripts.Objects.Electrical;
using Assets.Scripts.Objects.Pipes;
using Core;
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
        static MinMaxConfig AirCond_Config => new MinMaxConfig(Mod.AirConditioner_MinPower, Mod.AirConditioner_MaxPower);
        static MinMaxConfig TurboPump_Config => new MinMaxConfig(Mod.TurboVolumePump_MinPower, Mod.TurboVolumePump_MaxPower);

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
                TurboPump_Config.CheckConfig(nameof(PowerTick_ConsumePower));
                TurboPump_Config.Assign(ref powerRequired);
            }

            if (device is AirConditioner)
            {
                AirCond_Config.CheckConfig(nameof(PowerTick_ConsumePower));
                AirCond_Config.Assign(ref powerRequired);
            }
		}
    }
}
