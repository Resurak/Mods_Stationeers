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
        public const string pluginVersion = "0.9";

        // Plugin configs
        public static ConfigHandler ConfigHandler;

        public static ConfigFloat Cable_MaxVoltage;

        public static ConfigFloat AirConditioner_MinPower;
        public static ConfigFloat AirConditioner_MaxPower;
        public static ConfigMinMax AirConditioner_MinMax;

        public static ConfigFloat TurboPumpGas_MinPower;
        public static ConfigFloat TurboPumpGas_MaxPower;
        public static ConfigMinMax TurboPumpGas_MinMax;

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
            ConfigHandler = new ConfigHandler(pluginGuid);

            Cable_MaxVoltage = ConfigHandler.AddFloat("Cable", "MaxVoltage", "Max voltage that a cable can absorb before breaking", 5000f);

            AirConditioner_MinPower = ConfigHandler.AddFloat("AirConditioner", "MinPower", "Minimum power draw", 100f);
            AirConditioner_MaxPower = ConfigHandler.AddFloat("AirConditioner", "MaxPower", "Maximum power draw", 1500f);
            AirConditioner_MinMax = new ConfigMinMax(ConfigHandler, AirConditioner_MinPower, AirConditioner_MaxPower);

            TurboPumpGas_MinPower = ConfigHandler.AddFloat("TurboPumpGas", "MinPower", "Minimum power draw", 50f);
            TurboPumpGas_MaxPower = ConfigHandler.AddFloat("TurboPumpGas", "MaxPower", "Maximum power draw", 200f);
            TurboPumpGas_MinMax = new ConfigMinMax(ConfigHandler, TurboPumpGas_MinPower, TurboPumpGas_MaxPower);
        }

        public void Patch()
        {
            Log.LogInfo("Patching");

            Harmony harmony = new Harmony(pluginGuid);
            harmony.PatchAll();
        }
    }
}
