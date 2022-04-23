using Assets.Scripts.Objects.Items;
using Core.Shared;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfficientTools
{
    public class ModPatches
    {
        static float PowerTool_StoredPower = 0f;
        static float MiningDrill_StoredPower = 0f;

        static PowerRange ArcWelder_PowerRange = new PowerRange(Mod.ArcWelder_PowerToUse);
        static PowerRange PowerTool_PowerRange = new PowerRange(Mod.PowerTool_PowerToUse);
        static PowerRange MiningDrill_PowerRange = new PowerRange(Mod.MiningDrill_PowerToUse);

        [HarmonyPatch(typeof(ArcWeldingTool), nameof(ArcWeldingTool.OnPowerTick))]
        [HarmonyPrefix]
        static void ArcWelder_OnPowerTick_prefix(ArcWeldingTool __instance)
        {
            if (ArcWelder_PowerRange.Valid)
            {
                __instance.UsedPowerPassive = ArcWelder_PowerRange.Max;
            }
        }

        [HarmonyPatch(typeof(MiningDrill), nameof(MiningDrill.OnMinedOre))]
        [HarmonyPrefix]
        static void MiningDrill_OnMinedOre_Prefix(MiningDrill __instance)
        {
            if (MiningDrill_PowerRange.Valid)
            {
                MiningDrill_StoredPower = __instance.Battery.PowerStored;
            }
        }

        [HarmonyPatch(typeof(MiningDrill), nameof(MiningDrill.OnMinedOre))]
        [HarmonyPostfix]
        static void MiningDrill_OnMinedOre_Postfix(MiningDrill __instance)
        {
            if (MiningDrill_PowerRange.Valid)
            {
                Mod.Log.LogInfo("Before: " + MiningDrill_StoredPower);

                __instance.Battery.PowerStored = MiningDrill_StoredPower;
                __instance.Battery.PowerStored -= MiningDrill_PowerRange.Max;

                Mod.Log.LogInfo("After: " + __instance.Battery.PowerStored);
            }
        }

        [HarmonyPatch(typeof(PowerTool), nameof(PowerTool.OnUseItem))]
        [HarmonyPrefix]
        static void PowerTool_OnUseItem_Prefix(PowerTool __instance)
        {
            if (PowerTool_PowerRange.Valid)
            {
                PowerTool_StoredPower = __instance.Battery.PowerStored;
            }
        }

        [HarmonyPatch(typeof(PowerTool), nameof(PowerTool.OnUseItem))]
        [HarmonyPostfix]
        static void PowerTool_OnUseItem_Postfix(PowerTool __instance)
        {
            if (PowerTool_PowerRange.Valid)
            {
                __instance.Battery.PowerStored = PowerTool_StoredPower;
                __instance.Battery.PowerStored -= PowerTool_PowerRange.Max;
            }
        }
    }
}
