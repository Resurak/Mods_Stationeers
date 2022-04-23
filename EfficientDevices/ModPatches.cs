using Assets.Scripts.Networks;
using Assets.Scripts.Objects.Electrical;
using Assets.Scripts.Objects.Pipes;
using Core.Shared;
using HarmonyLib;
using Objects.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfficientDevices
{
    public class ModPatches
    {
        static PowerRange Cable_VoltageRange = new PowerRange(Mod.Cable_MaxVoltage);
        static PowerRange TurboPumpGas_PowerRange = new PowerRange(Mod.TurboPumpGas_MinPower, Mod.TurboPumpGas_MaxPower);
        static PowerRange AirConditioner_PowerRange = new PowerRange(Mod.AirConditioner_MinPower, Mod.AirConditioner_MaxPower);

        [HarmonyPatch(typeof(Cable), MethodType.Constructor)]
        [HarmonyPostfix]
        static void Cable_Constructor(Cable __instance)
        {
            if (Cable_VoltageRange.Valid)
            {
                __instance.MaxVoltage = Cable_VoltageRange.Max;
            }
        }

        [HarmonyPatch(typeof(AirConditioner), nameof(AirConditioner.GetUsedPower))]
        [HarmonyPostfix]
        static void AirConditioner_GetUsedPower_Postfix(ref float __result)
        {
            __result.AssignPower(AirConditioner_PowerRange);
        }

        [HarmonyPatch(typeof(TurboVolumePump), nameof(TurboVolumePump.GetUsedPower))]
        [HarmonyPostfix]
        static void TurboPumpGas_GetUsedPower_Postfix(ref float __result)
        {
            __result.AssignPower(TurboPumpGas_PowerRange);
        }

        [HarmonyPatch(typeof(PowerTick), "ConsumePower")]
        [HarmonyPrefix]
        static void PowerTick_ConsumePower_Prefix(Device device, ref float powerRequired)
        {
            if (device is TurboVolumePump)
            {
                powerRequired.AssignPower(TurboPumpGas_PowerRange);
            }

            if (device is AirConditioner)
            {
                powerRequired.AssignPower(AirConditioner_PowerRange);
            }
        }
    }
}
