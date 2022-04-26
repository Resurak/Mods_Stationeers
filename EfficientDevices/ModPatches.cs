using Assets.Scripts.Atmospherics;
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
using UnityEngine;

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

        //[HarmonyPatch(typeof(AirConditioner), nameof(AirConditioner.OnButtonIncrease))]
        //[HarmonyPostfix]
        //static void AirConditioner_OnButtonIncrease_Postfix(AirConditioner __instance)
        //{
        //    __instance.GoalTemperature += KeyManager.GetButton(KeyMap.QuantityModifier) ? 1 : 10;
        //}

        //[HarmonyPatch(typeof(AirConditioner), nameof(AirConditioner.OnButtonDecrease))]
        //[HarmonyPostfix]
        //static void AirConditioner_OnButtonDecrease_Postfix(AirConditioner __instance)
        //{
        //    __instance.GoalTemperature -= KeyManager.GetButton(KeyMap.QuantityModifier) ? 1 : 10;
        //}

        [HarmonyPatch(typeof(AirConditioner), nameof(AirConditioner.MachineCommand))]
        [HarmonyPrefix]
        static bool AirConditioner_MachineCommand_Prefix(AirConditioner __instance, int referenceInt)
        {
            __instance.GoalTemperature += referenceInt;
            return false;
        }

        [HarmonyPatch(typeof(AirConditioner), nameof(AirConditioner.GetUsedPower))]
        [HarmonyPostfix]
        static void AirConditioner_GetUsedPower_Postfix(ref float __result)
        {
            if (Mod.AirConditioner_EasyMode.Value)
            {
                __result = Mod.AirConditioner_MaxPower.Value;
            }
            else
            {
                __result.AssignPower(AirConditioner_PowerRange);
            }
        }

        [HarmonyPatch(typeof(AirConditioner), nameof(AirConditioner.OnAtmosphericTick))]
        [HarmonyPrefix]
        static bool AirConditioner_OnAtmoshpericTick_Prefix(AirConditioner __instance, ref float ____powerUsedDuringTick)
        {
            if (!Mod.AirConditioner_EasyMode.Value)
            {
                return true;
            }
            else
            {
                if (__instance.OnOff && __instance.Powered && __instance.InputNetwork != null && __instance.InputNetwork.Atmosphere != null)
                {
                    var inputAtm = __instance.InputNetwork.Atmosphere;
                    var currentPa = inputAtm?.PressureGassesAndLiquidsInPa;

                    if (inputAtm == null || currentPa == null || currentPa < 10f)
                    {
                        return false;
                    }

                    var tempDiff = Math.Abs(inputAtm.Temperature - __instance.GoalTemperature);
                    var power = 0f;

                    if (tempDiff > 100f)
                    {
                        power = (float)currentPa / 2;
                    }
                    else if (tempDiff > 50f)
                    {
                        power = (float)currentPa / 10;
                    }
                    else if (tempDiff > 10f)
                    {
                        power = (float)currentPa / 25;
                    } 
                    else if (tempDiff > 5f)
                    {
                        power = (float)currentPa / 50;
                    }
                    else if (tempDiff > 2f)
                    {
                        power = (float)currentPa / 100;
                    }
                    else if (tempDiff > 0.2f)
                    {
                        power = (float)currentPa / 200;
                    }
                    else if (tempDiff <= 0.2f && tempDiff > 0.05f)
                    {
                        power = (float)currentPa / 500;
                    }
                    else
                    {
                        __instance.InputNetwork.Atmosphere = inputAtm;
                        ____powerUsedDuringTick = 1f;

                        return false;
                    }

                    if (inputAtm.Temperature < __instance.GoalTemperature)
                    {
                        inputAtm.GasMixture.AddEnergy(power);
                    }
                    else
                    {
                        inputAtm.GasMixture.RemoveEnergy(power);
                    }

                    __instance.InputNetwork.Atmosphere = inputAtm;
                    ____powerUsedDuringTick = 1f;

                    return false;
                }

                ____powerUsedDuringTick = 1f;
                return false;
            }
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
                if (Mod.AirConditioner_EasyMode.Value)
                {
                    powerRequired = Mod.AirConditioner_MaxPower.Value;
                }
                else
                {
                    powerRequired.AssignPower(AirConditioner_PowerRange);
                }
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
            var autoOnOff = Mod.PipeHeater_AutoOnOff.Value;
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
