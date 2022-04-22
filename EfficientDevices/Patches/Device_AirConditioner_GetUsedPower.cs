using Assets.Scripts.Networks;
using Assets.Scripts.Objects.Electrical;
using Core.Shared;
using HarmonyLib;

namespace EfficientDevices.Patches
{
    [HarmonyPatch(typeof(AirConditioner), nameof(AirConditioner.GetUsedPower))]
    public class Device_AirConditioner_GetUsedPower
    {
        /// <summary>
        /// Patches AirConditioner.GetUsedPower to diplay the correct value
        /// </summary>
        /// <param name="__result"></param>
        static void Postfix(ref float __result)
        {
            __result.AssignConfigValue(Mod.AirConditioner_MinMax);
        }
    }
}
