using Assets.Scripts.Objects;
using Assets.Scripts.Objects.Items;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfficientTools.Patches
{
    [HarmonyPatch(typeof(PowerTool), nameof(PowerTool.OnUseItem))]
    public class PowerTool_OnUseItem
    {
        protected internal static float storedPower = 0f;

        /// <summary>
        /// Patches PowerTool.OnUseItem to store current battery charge
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="onUseThing"></param>
        /// <param name="__instance"></param>
        static void Prefix(float quantity, Thing onUseThing, PowerTool __instance)
        {
            storedPower = __instance.Battery.PowerStored;
        }

        /// <summary>
        /// Patches PowerTool.OnUseItem to restore the previous stored power and subtract the configured mod value
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="onUseThing"></param>
        /// <param name="__instance"></param>
        static void Postfix(float quantity, Thing onUseThing, PowerTool __instance)
        {
            __instance.Battery.PowerStored = storedPower;
            __instance.Battery.PowerStored -= quantity >= Mod.PowerTool_PowerToUse.Value ? Mod.PowerTool_PowerToUse.Value : quantity;
        }
    }
}
