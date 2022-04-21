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
	[BepInPlugin(pluginGuid, pluginName, pluginVersion)]
	public class Mod : BaseUnityPlugin
	{
		public const string pluginGuid = "resurak.EfficientTools";
		public const string pluginName = "Efficient Tools";
		public const string pluginVersion = "0.3";

		public static ConfigEntry<float> PowerTool_PowerToUse;
		public static ConfigEntry<float> ArcWelder_PowerToUse;
		public static ConfigEntry<float> MiningDrill_PowerToUse;

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

			Log.LogInfo("Patching");
			harmony.PatchAll();

			Log.LogInfo("Loaded correclty");
		}

		public void LoadConfig()
        {
			PowerTool_PowerToUse = Config.Bind(
				"Power",
				"PowerTool",
				360f,
				"Power usage of power tools (hand drill, angle grinder)");

			ArcWelder_PowerToUse = Config.Bind(
				"Power",
				"Arc welder",
				720f,
				"Power usage of arc welder");

			MiningDrill_PowerToUse = Config.Bind(
				"Power",
				"Mining drill",
				720f,
				"Power usage of mining drill");
        }
	}
}
