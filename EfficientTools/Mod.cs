using BepInEx;
using BepInEx.Logging;
using Core.Config;
using HarmonyLib;

namespace EfficientTools
{
    // Token: 0x02000002 RID: 2
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
	[BepInDependency(coreGuid)]
	public class Mod : BaseUnityPlugin
	{
		// Plugin info
		public const string coreGuid = "resurak.Core";
		public const string pluginGuid = "resurak.EfficientTools";
		public const string pluginName = "Efficient Tools";
		public const string pluginVersion = "0.9";

		// Plugin configs
		public static ConfigHandler ConfigHandler;

		public static ConfigFloat PowerTool_PowerToUse;
		public static ConfigFloat ArcWelder_PowerToUse;
		public static ConfigFloat MiningDrill_PowerToUse;

		// Plugin logger
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
			ConfigHandler = new ConfigHandler(pluginGuid, Config);

			PowerTool_PowerToUse = ConfigHandler.LoadFloat("PowerTool", "PowerUsed", "Power use of tools", 360f);
			ArcWelder_PowerToUse = ConfigHandler.LoadFloat("ArcWelder", "PowerUsed", "Power use of each weld", 720f);
			MiningDrill_PowerToUse = ConfigHandler.LoadFloat("MiningDrill", "PowerUsed", "Power use of each ore mined", 720f);
		}

		public void Patch()
        {
			Log.LogInfo("Patching");
			Harmony.CreateAndPatchAll(typeof(ModPatches), pluginGuid);
		}
	}
}
