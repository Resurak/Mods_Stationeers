using Assets.Scripts.Objects.Items;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfficientTools.Patches
{
    [HarmonyPatch(typeof(MiningDrill), nameof(MiningDrill.OnMinedOre))]
    public class PowerTool_MiningDrill_OnMinedOre
    {
        static float power => Mod.MiningDrill_PowerToUse.Value > 5 ? Mod.MiningDrill_PowerToUse.Value : 5;
        protected internal static float storedPower = 0f;

        /// <summary>
        /// Patches MiningDrill.OnMinedOre to store current battery charge
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="oreMined"></param>
        static void Prefix(MiningDrill __instance, Ore oreMined)
        {
            storedPower = __instance.Battery.PowerStored;
        }

        /// <summary>
        /// Patches MiningDrill.OnMinedOre to restore the previous stored power and subtract the configured mod value
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="oreMined"></param>
        static void Postfix(MiningDrill __instance, Ore oreMined)
        {
            __instance.Battery.PowerStored = storedPower;
            __instance.Battery.PowerStored -= power;
        }
    }
}
