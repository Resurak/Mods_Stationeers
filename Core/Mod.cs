using BepInEx;
using Core.Config;
using Core.Shared;

namespace Core
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Mod : BaseUnityPlugin
    {
        // Plugin info
        public const string pluginGuid = "resurak.Core";
        public const string pluginName = "Resurak Mod Core";
        public const string pluginVersion = "0.8";

        // Plugin configs
        public static ConfigBool Log_ShowConfigLoad;
        public static ConfigBool Log_ShowConfigValid;

        // Plugin logger

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

            CoreLogger.Info("Mod loaded correclty");
        }

        public void CreateLogger()
        {
            CoreLogger.Log = base.Logger;
            CoreLogger.Info("Loading mod");
        }

        public void LoadConfig()
        {
            CoreLogger.Info("Loading config");

            Log_ShowConfigLoad = new ConfigBool(pluginGuid, "Logging", "ShowConfigLoad", "If enabled, logs the loading of plugin configs", true);
            Log_ShowConfigValid = new ConfigBool(pluginGuid, "Logging", "ShowConfigValid", "If enabled, logs if a config is valid or not", true);
        }

        public void Patch()
        {
            CoreLogger.Warn("No methods to patch");
        }
    }
}
