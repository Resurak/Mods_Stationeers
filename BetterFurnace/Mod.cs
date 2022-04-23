using BepInEx;
using BepInEx.Logging;
using Core.Config;
using HarmonyLib;

namespace BetterFurnace
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInDependency(coreGuid)]
    public class Mod : BaseUnityPlugin
    {
        // Plugin info
        public const string coreGuid = "resurak.Core";
        public const string pluginGuid = "resurak.BetterFurnace";
        public const string pluginName = "Better Furnace";
        public const string pluginVersion = "0.12";

        // Plugin configs
        public static ConfigHandler ConfigHandler;

        public static ConfigFloat Furnace_MinSetting;
        public static ConfigFloat Furnace_MaxSetting;

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

            Furnace_MinSetting = ConfigHandler.LoadFloat("Furnace", "MinSetting", "Minimum Setting value for the furnace", 1f);
            Furnace_MaxSetting = ConfigHandler.LoadFloat("Furnace", "MaxSetting", "Maximum Setting value for the furnace", 100f);
        }

        public void Patch()
        {
            Log.LogInfo("Patching");
            Harmony.CreateAndPatchAll(typeof(ModPatches), pluginGuid);
        }
    }
}
