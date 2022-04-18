using System;
using Assets.Scripts.Objects;
using Assets.Scripts.Objects.Items;

namespace EfficientTools
{
	public class Patches
	{
        protected internal static float PowerTool_StoredPower = 0f;
        protected internal static float MiningDrill_StoredPower = 0f;

		public static void PowerTool_OnUseItem_Prefix(float quantity, Thing onUseThing, PowerTool __instance)
        {
			PowerTool_StoredPower = __instance.Battery.PowerStored;
        }

		public static void PowerTool_OnUseItem_Postfix(float quantity, Thing onUseThing, PowerTool __instance)
        {
			__instance.Battery.PowerStored = PowerTool_StoredPower;
			__instance.Battery.PowerStored -= quantity >= Mod.PowerTool_PowerToUse.Value ? Mod.PowerTool_PowerToUse.Value : quantity;
        }

        public static void MiningDrill_OnMinedOre_Prefix(MiningDrill __instance, Ore oreMined)
        {
            MiningDrill_StoredPower = __instance.Battery.PowerStored;
        }

        public static void MiningDrill_OnMinedOre_Postfix(MiningDrill __instance, Ore oreMined)
        {
            __instance.Battery.PowerStored = MiningDrill_StoredPower;
            __instance.Battery.PowerStored -= Mod.PowerTool_PowerToUse.Value;
        }

        public static void ArcWeldingTool_OnPowerTick_Prefix(ArcWeldingTool __instance)
        {
            __instance.UsedPowerPassive = Mod.ArcWelder_PowerToUse.Value;
        }
    }
}