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
        public const string pluginVersion = "0.12";

        // Plugin configs
        public static ConfigHandler ConfigHandler;

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
            ConfigHandler = new ConfigHandler(pluginGuid, Config);

            Log_ShowConfigLoad = ConfigHandler.LoadBool("Logging", "ShowConfigLoad", "If enabled, logs the loading of plugin configs", true);
            Log_ShowConfigValid = ConfigHandler.LoadBool("Logging", "ShowConfigValid", "If enabled, logs if a config is valid or not", true);
        }

        public void Patch()
        {
            CoreLogger.Warn("No methods to patch");
        }
    }
}
