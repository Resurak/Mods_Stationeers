using Assets.Scripts.Objects.Items;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfficientTools
{
    [HarmonyPatch(typeof(MiningDrill), nameof(MiningDrill.OnMinedOre))]
    public class MiningDrill_OnMinedOre
    {
        protected internal static float storedPower = 0f;

        static void Prefix(MiningDrill __instance, Ore oreMined)
        {
            storedPower = __instance.Battery.PowerStored;
        }

        static void Postfix(MiningDrill __instance, Ore oreMined)
        {
            __instance.Battery.PowerStored = storedPower;
            __instance.Battery.PowerStored -= Mod.PowerTool_PowerToUse.Value;
        }
    }
}
