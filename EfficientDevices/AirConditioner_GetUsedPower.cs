using Assets.Scripts.Networks;
using Assets.Scripts.Objects.Electrical;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfficientDevices
{
    [HarmonyPatch(typeof(AirConditioner), nameof(AirConditioner.GetUsedPower))]
    public class AirConditioner_GetUsedPower
    {
        static void Postfix(CableNetwork cableNetwork, ref float __result)
        {
            if (__result > Mod.AirConditioner_MaxPower.Value)
            {
                __result = Mod.AirConditioner_MaxPower.Value;
            }
        }
    }
}
