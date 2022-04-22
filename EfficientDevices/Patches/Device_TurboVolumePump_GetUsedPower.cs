using Assets.Scripts.Networks;
using Core.Shared;
using HarmonyLib;
using Objects.Pipes;

namespace EfficientDevices.Patches
{
    [HarmonyPatch(typeof(TurboVolumePump), nameof(TurboVolumePump.GetUsedPower))]
    public class Device_TurboVolumePump_GetUsedPower
    {
        /// <summary>
        /// Patches TurboVolumePump.GetUsedPower to diplay the correct value
        /// </summary>
        /// <param name="__result"></param>
        static void Postfix(ref float __result)
        {
            __result.AssignConfigValue(Mod.TurboPumpGas_MinMax);
        }
    }
}
