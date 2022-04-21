using Assets.Scripts.Networks;
using Assets.Scripts.Objects.Electrical;
using Core;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfficientDevices.Patches
{
    [HarmonyPatch(typeof(AirConditioner), nameof(AirConditioner.GetUsedPower))]
    public class Device_AirConditioner_GetUsedPower
    {
        static MinMaxConfig Config => new MinMaxConfig(Mod.AirConditioner_MinPower, Mod.AirConditioner_MaxPower);

        /// <summary>
        /// Patches AirConditioner.GetUsedPower to diplay the correct value
        /// </summary>
        /// <param name="cableNetwork"></param>
        /// <param name="__result"></param>
        static void Postfix(CableNetwork cableNetwork, ref float __result)
        {
            Config.CheckConfig(nameof(Device_AirConditioner_GetUsedPower));
            Config.Assign(ref __result);
        }
    }
}
