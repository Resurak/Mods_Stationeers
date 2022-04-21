using Assets.Scripts.Objects.Items;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfficientTools.Patches
{
    [HarmonyPatch(typeof(ArcWeldingTool), nameof(ArcWeldingTool.OnPowerTick))]
    public class PowerTool_ArcWeldingTool_OnPowerTick
    {
        static void Prefix(ArcWeldingTool __instance)
        {
            __instance.UsedPowerPassive = Mod.ArcWelder_PowerToUse.Value;
        }
    }
}
