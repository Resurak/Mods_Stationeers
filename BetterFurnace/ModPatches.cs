using Assets.Scripts.Atmospherics;
using Assets.Scripts.Objects;
using Assets.Scripts.Objects.Items;
using Assets.Scripts.Objects.Pipes;
using Core.Shared;
using HarmonyLib;
using Reagents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterFurnace
{
    public class ModPatches
    {
        static PowerRange Furnace_SettingRange = new PowerRange(Mod.Furnace_MinSetting, Mod.Furnace_MaxSetting, 1f);

        [HarmonyPatch(typeof(FurnaceBase), MethodType.Constructor)]
        [HarmonyPostfix]
        static void FurnaceBase_Constructor(FurnaceBase __instance)
        {
            float setting = 1f;
            setting.AssignPower(Furnace_SettingRange, 1f);

            __instance.OutputSetting = setting;
        }

        [HarmonyPatch(typeof(FurnaceBase), nameof(FurnaceBase.Smelt))]
        [HarmonyPrefix]
        static bool FurnaceBase_Smelt_Prefix(DynamicThing dynamicThing, FurnaceBase __instance, Atmosphere ___InternalAtmosphere, ReagentMixture ___ReagentMixture, ref bool __result)
        {
            float setting = (float)Math.Round(__instance.OutputSetting);
            setting.AssignPower(Furnace_SettingRange, 1f);

            for (int i = 0; i < setting; i++)
            {
                IQuantity size = dynamicThing as IQuantity;

                if (size == null)
                {
                    Mod.Log.LogError("DynamicThing as IQuantity returned null. Probably a bug");

                    __result = false;
                    return false;
                }
                if (size.GetQuantity == 0)
                {
                    Mod.Log.LogWarning("DynamicThing as IQuantity equal to 0, breaking loop");

                    __result = false;
                    return false;
                }

                dynamicThing.Smelt(___InternalAtmosphere, ___ReagentMixture);
            }

            __result = true;
            return false;
        }
    }
}
