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
        static bool validConfig = false;
        static bool checkedConfig = false;

        static MinMax Power => MinMax.New(Mod.AirConditioner_MinPower, Mod.AirConditioner_MaxPower);

        /// <summary>
        /// Patches AirConditioner.GetUsedPower to diplay the correct value
        /// </summary>
        /// <param name="cableNetwork"></param>
        /// <param name="__result"></param>
        static void Postfix(CableNetwork cableNetwork, ref float __result)
        {
            if (!checkedConfig)
            {
                validConfig = Utils.ConfigIsValid(typeof(Device_AirConditioner_GetUsedPower), Power);
                checkedConfig = true;
            }

            if (validConfig)
            {
                Utils.AssignMinMax(ref __result, Power);
            }
        }
    }
}
