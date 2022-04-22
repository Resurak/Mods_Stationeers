﻿using Assets.Scripts.Objects.Items;
using HarmonyLib;

namespace EfficientTools.Patches
{
    [HarmonyPatch(typeof(ArcWeldingTool), nameof(ArcWeldingTool.OnPowerTick))]
    public class PowerTool_ArcWeldingTool_OnPowerTick
    {
        static float power => Mod.ArcWelder_PowerToUse.Value > 5 ? Mod.ArcWelder_PowerToUse.Value : 5;

        /// <summary>
        /// Patches ArcWeldingTool.OnPowerTick to set the configured mod value
        /// </summary>
        /// <param name="__instance"></param>
        static void Prefix(ArcWeldingTool __instance)
        {
            __instance.UsedPowerPassive = power;
        }
    }
}
