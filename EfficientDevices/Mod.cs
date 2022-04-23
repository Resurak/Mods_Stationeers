using BepInEx;
using BepInEx.Logging;
using Core.Config;
using HarmonyLib;

namespace EfficientDevices
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInDependency(coreGuid)]
    public class Mod : BaseUnityPlugin
    {
        // Plugin info
        public const string coreGuid = "resurak.Core";
        public const string pluginGuid = "resurak.EfficientDevices";
        public const string pluginName = "Efficient Devices";
        public const string pluginVersion = "0.12";

        // Plugin configs
        public static ConfigHandler ConfigHandler;

        public static ConfigFloat Cable_MaxVoltage;

        public static ConfigFloat AirConditioner_MinPower;
        public static ConfigFloat AirConditioner_MaxPower;

        public static ConfigFloat TurboPumpGas_MinPower;
        public static ConfigFloat TurboPumpGas_MaxPower;

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

            Cable_MaxVoltage = ConfigHandler.LoadFloat("Cable", "MaxVoltage", "Max voltage that a cable can absorb before breaking", 5000f);

            AirConditioner_MinPower = ConfigHandler.LoadFloat("AirConditioner", "MinPower", "Minimum power draw", 100f);
            AirConditioner_MaxPower = ConfigHandler.LoadFloat("AirConditioner", "MaxPower", "Maximum power draw", 1500f);

            TurboPumpGas_MinPower = ConfigHandler.LoadFloat("TurboPumpGas", "MinPower", "Minimum power draw", 50f);
            TurboPumpGas_MaxPower = ConfigHandler.LoadFloat("TurboPumpGas", "MaxPower", "Maximum power draw", 200f);
        }

        public void Patch()
        {
            Log.LogInfo("Patching");
            Harmony.CreateAndPatchAll(typeof(ModPatches), pluginGuid);
        }
    }
}
