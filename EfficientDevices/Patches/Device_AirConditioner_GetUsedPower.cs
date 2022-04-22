using Assets.Scripts.Networks;
using Assets.Scripts.Objects.Electrical;
using Core;
using Core.Shared;
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
        /// <summary>
        /// Patches AirConditioner.GetUsedPower to diplay the correct value
        /// </summary>
        /// <param name="cableNetwork"></param>
        /// <param name="__result"></param>
        static void Postfix(CableNetwork cableNetwork, ref float __result)
        {
            Utils.MinMax(ref __result, Mod.AirConditioner_Config);
        }
    }
}
