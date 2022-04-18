using System;
using System.Reflection;
using Assets.Scripts.Objects.Items;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace EfficientTools
{
	// Token: 0x02000002 RID: 2
	[BepInPlugin("resurak.EfficientTools", "Efficient Tools", "0.1")]
	public class Mod : BaseUnityPlugin
	{
		public const string pluginGuid = "resurak.EfficientTools";
		public const string pluginName = "Efficient Tools";
		public const string pluginVersion = "0.1";

		public static ConfigEntry<float> PowerTool_PowerToUse;
		public static ConfigEntry<float> ArcWelder_PowerToUse;

		internal static ManualLogSource Log;

		public void Awake()
		{
			// Creating logger
			Log = base.Logger;
			Log.LogInfo("Loading mod");

			// Load configs
			Log.LogInfo("Loading config");
			LoadConfig();

			// Creating harmony instance
			Harmony harmony = new Harmony(pluginGuid);

			// Getting original method from game and patched method
			var PowerTool_OnUseItem = typeof(PowerTool).GetMethod(nameof(PowerTool.OnUseItem));
			var PowerTool_OnUseItem_Prefix = typeof(Patches).GetMethod(nameof(Patches.PowerTool_OnUseItem_Prefix));
			var PowerTool_OnUseItem_Postfix = typeof(Patches).GetMethod(nameof(Patches.PowerTool_OnUseItem_Postfix));

			var MiningDrill_OnMinedOre = typeof(MiningDrill).GetMethod(nameof(MiningDrill.OnMinedOre));
			var MiningDrill_OnMinedOre_Prefix = typeof(Patches).GetMethod(nameof(Patches.MiningDrill_OnMinedOre_Prefix));
			var MiningDrill_OnMinedOre_Postfix = typeof(Patches).GetMethod(nameof(Patches.MiningDrill_OnMinedOre_Postfix));

			var ArcWeldingTool_OnPowerTick = typeof(ArcWeldingTool).GetMethod(nameof(ArcWeldingTool.OnPowerTick));
			var ArcWeldingTool_OnPowerTick_Prefix = typeof(Patches).GetMethod(nameof(Patches.ArcWeldingTool_OnPowerTick_Prefix));

			// Patching 
			Log.LogInfo("Patching PowerTool.OnUseItem");
			harmony.Patch(PowerTool_OnUseItem, new HarmonyMethod(PowerTool_OnUseItem_Prefix), new HarmonyMethod(PowerTool_OnUseItem_Postfix));

			Log.LogInfo("Patching MiningDrill.OnMinedOre");
			harmony.Patch(MiningDrill_OnMinedOre, new HarmonyMethod(MiningDrill_OnMinedOre_Prefix), new HarmonyMethod(MiningDrill_OnMinedOre_Postfix));

			Log.LogInfo("Patching ArcWeldingTool.OnPowerTick");
			harmony.Patch(ArcWeldingTool_OnPowerTick, prefix: new HarmonyMethod(ArcWeldingTool_OnPowerTick_Prefix));

			Log.LogInfo("Loaded correclty");
		}

		public void LoadConfig()
        {
			PowerTool_PowerToUse = Config.Bind(
				"Power",
				"PowerTool_PowerToUse",
				360f,
				"Power usage of power tools (hand drill, mining drill, angle grinder)");

			ArcWelder_PowerToUse = Config.Bind(
				"Power",
				"ArcWelder_PowerToUse",
				720f,
				"Power usage of Arc welder");
        }
	}
}
