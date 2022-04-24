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
        static PowerRange TurboPumpGas_PowerRange = new PowerRange(Mod.TurboPump_MinPower, Mod.TurboPump_MaxPower);
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
        static void TurboPump_GetUsedPower_Postfix(ref float __result)
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

        [HarmonyPatch(typeof(PipeHeater), MethodType.Constructor)]
        [HarmonyPostfix]
        static void PipeHeater_Constructor(PipeHeater __instance)
        {
            //__instance.UsedPower = 0f;
            __instance.HeatTransferJoulesPerTick = Mod.PipeHeater_HeatPower.Value;
        }

        [HarmonyPatch(typeof(PipeHeater), nameof(PipeHeater.OnAtmosphericTick))]
        [HarmonyPrefix]
        static bool PipeHeater_OnAtmosphericTick_Prefix(PipeHeater __instance, ref float ____powerUsedDuringTick)
        {
            // Remove heat dispersion
            // base.OnAtmosphericTick();

            var currentPa = __instance.NetworkAtmosphere.PressureGassesAndLiquidsInPa;
            var currentTemp = __instance.NetworkAtmosphere.Temperature;

            var advanced = Mod.PipeHeater_Advanced.Value;
            var usedPower = Mod.PipeHeater_UsedPower.Value;
            var heatPower = Mod.PipeHeater_HeatPower.Value;
            var autoOnOff = Mod.PipeHeater_OnOffOnTemp.Value;
            var autoHeatP = Mod.PipeHeater_AutoHeatPower.Value;
            var desiredTemp = Mod.PipeHeater_DesiredTemp.Value + 273f;

            if (advanced)
            {
                if (currentTemp <= desiredTemp)
                {
                    if (autoOnOff && !__instance.OnOff)
                    {
                        __instance.OnOff = true;
                    }
                    else if (!__instance.OnOff || !__instance.Powered)
                    {
                        return false;
                    }

                    if (__instance.NetworkAtmosphere == null || !__instance.NetworkAtmosphere.IsValid() || !__instance.HasOpenGrid)
                    {
                        return false;
                    }

                    __instance.NetworkAtmosphere.GasMixture.AddEnergy(autoHeatP ? currentPa / 100 : heatPower);
                    __instance.UsedPower = usedPower;
                    ____powerUsedDuringTick = usedPower;
                }
                else
                {
                    if (autoOnOff && __instance.OnOff)
                    {
                        __instance.OnOff = false;
                    }
                }

                return false;
            }

            if (!__instance.OnOff || !__instance.Powered)
            {
                return false;
            }
            if (__instance.NetworkAtmosphere == null || !__instance.NetworkAtmosphere.IsValid() || !__instance.HasOpenGrid)
            {
                return false;
            }
            if (__instance.NetworkAtmosphere != null && __instance.NetworkAtmosphere.IsAboveArmstrong())
            {
                __instance.NetworkAtmosphere.GasMixture.AddEnergy(heatPower);
                __instance.UsedPower = usedPower;
                ____powerUsedDuringTick = usedPower;
            }

            return false;
        }
    }
}
