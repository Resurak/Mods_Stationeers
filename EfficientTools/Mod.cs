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
	[BepInDependency(coreGuid)]
	public class Mod : BaseUnityPlugin
	{
		public const string coreGuid = "resurak.Core";
		public const string pluginGuid = "resurak.EfficientTools";
		public const string pluginName = "Efficient Tools";
		public const string pluginVersion = "0.6";

		public static ConfigEntry<float> PowerTool_PowerToUse;
		public static ConfigEntry<float> ArcWelder_PowerToUse;
		public static ConfigEntry<float> MiningDrill_PowerToUse;

		internal static ManualLogSource Log;

		/// <summary>
		/// Called on mod load
		/// </summary>
		public void Awake()
		{
			// Creating logger
			CreateLogger();

			// Load configs
			LoadConfig();

			// Harmony patches
			Patch();

			Log.LogInfo("Mod loaded correclty");
		}

		public void CreateLogger()
        {
			Log = base.Logger;
			Log.LogInfo("Loading mod");
		}

		public void LoadConfig()
        {
			Log.LogInfo("Loading config");

			PowerTool_PowerToUse = Config.Bind(
				"Power Tool",
				"Power used",
				360f,
				"Power usage of power tools (hand drill, angle grinder)");
			Log.LogInfo($"{nameof(PowerTool_PowerToUse)} = {PowerTool_PowerToUse.Value}");

			ArcWelder_PowerToUse = Config.Bind(
				"Arc Welder",
				"Power used",
				720f,
				"Power usage of arc welder");
			Log.LogInfo($"{nameof(ArcWelder_PowerToUse)} = {ArcWelder_PowerToUse.Value}");

			MiningDrill_PowerToUse = Config.Bind(
				"Mining Drill",
				"Power used",
				720f,
				"Power usage of mining drill");
			Log.LogInfo($"{nameof(MiningDrill_PowerToUse)} = {MiningDrill_PowerToUse.Value}");
		}

		public void Patch()
        {
			Log.LogInfo("Patching");

			Harmony harmony = new Harmony(pluginGuid);
			harmony.PatchAll();
		}
	}
}
