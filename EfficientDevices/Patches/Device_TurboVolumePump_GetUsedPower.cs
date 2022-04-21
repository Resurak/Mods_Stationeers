using Assets.Scripts.Networks;
using Core;
using HarmonyLib;
using Objects.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfficientDevices.Patches
{
    [HarmonyPatch(typeof(TurboVolumePump), nameof(TurboVolumePump.GetUsedPower))]
    public class Device_TurboVolumePump_GetUsedPower
    {
        static MinMaxConfig Config => new MinMaxConfig(Mod.TurboVolumePump_MinPower, Mod.TurboVolumePump_MaxPower);

        /// <summary>
        /// Patches TurboVolumePump.GetUsedPower to diplay the correct value
        /// </summary>
        /// <param name="cableNetwork"></param>
        /// <param name="__result"></param>
        static void Postfix(CableNetwork cableNetwork, ref float __result)
        {
            Config.CheckConfig(nameof(Device_TurboVolumePump_GetUsedPower));
            Config.Assign(ref __result);
        }
    }
}
