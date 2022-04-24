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

        public static ConfigFloat TurboPump_MinPower;
        public static ConfigFloat TurboPump_MaxPower;

        public static ConfigFloat PipeHeater_UsedPower;
        public static ConfigFloat PipeHeater_HeatPower;

        public static ConfigBool PipeHeater_Advanced;
        public static ConfigBool PipeHeater_OnOffOnTemp;
        public static ConfigBool PipeHeater_AllowCooling;
        public static ConfigBool PipeHeater_AutoHeatPower;

        public static ConfigFloat PipeHeater_DesiredTemp;

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

            TurboPump_MinPower = ConfigHandler.LoadFloat("TurboPumpGas", "MinPower", "Minimum power draw", 50f);
            TurboPump_MaxPower = ConfigHandler.LoadFloat("TurboPumpGas", "MaxPower", "Maximum power draw", 200f);

            PipeHeater_UsedPower = ConfigHandler.LoadFloat("PipeHeater", "UsedPower", "Power draw", 1000f);
            PipeHeater_HeatPower = ConfigHandler.LoadFloat("PipeHeater", "HeatPower", "Heat power (Joules per tick)", 1000f);

            PipeHeater_Advanced = ConfigHandler.LoadBool("PipeHeater.Advanced", "Enabled", "Enables the advanced mode", false);
            PipeHeater_OnOffOnTemp = ConfigHandler.LoadBool("PipeHeater.Advanced", "OnOffOnTemp", "Power on/off the heater when the temp is below the setted temp", true);
            PipeHeater_AutoHeatPower = ConfigHandler.LoadBool("PipeHeater.Advanced", "AutoHeatPower", "Auto selects the best heat power to heat the gas (or liquid)", true);

            PipeHeater_DesiredTemp = ConfigHandler.LoadFloat("PipeHeater.Advanced", "DesiredTemp", "Desired temperature (in celsius)", 20f);
        }

        public void Patch()
        {
            Log.LogInfo("Patching");
            Harmony.CreateAndPatchAll(typeof(ModPatches), pluginGuid);
        }
    }
}
