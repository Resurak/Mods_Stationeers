using Assets.Scripts.Objects.Electrical;
using Assets.Scripts.Objects.Pipes;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Core;
using Core.Config;
using Core.Shared;
using HarmonyLib;
using Objects.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
        public const string pluginVersion = "0.8";

        // Plugin config values
        public static ConfigFloat Cable_MaxVoltage;

        public static ConfigFloat AirConditioner_MinPower;
        public static ConfigFloat AirConditioner_MaxPower;
        public static ConfigMinMax AirConditioner_Config;

        public static ConfigFloat TurboVolumePump_MinPower;
        public static ConfigFloat TurboVolumePump_MaxPower;
        public static ConfigMinMax TurboVolumePump_Config;

        // Mod logger
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

            Cable_MaxVoltage = new ConfigFloat(pluginGuid, "Cable", "MaxVoltage", "Max voltage that a cable can absorb before breaking", 5000f);

            AirConditioner_MinPower = new ConfigFloat(pluginGuid, "AirConditioner", "MinPower", "Minimum power draw", 100f);
            AirConditioner_MaxPower = new ConfigFloat(pluginGuid, "AirConditioner", "MaxPower", "Maximum power draw", 1500f);

            TurboVolumePump_MinPower = new ConfigFloat(pluginGuid, "TurboPumpGas", "MinPower", "Minimum power draw", 50f);
            TurboVolumePump_MaxPower = new ConfigFloat(pluginGuid, "TurboPumpGas", "MaxPower", "Maximum power draw", 200f);

            AirConditioner_Config = new ConfigMinMax(nameof(AirConditioner_Config), pluginGuid, AirConditioner_MinPower, AirConditioner_MaxPower);
            TurboVolumePump_Config = new ConfigMinMax(nameof(TurboVolumePump_Config), pluginGuid, TurboVolumePump_MinPower, TurboVolumePump_MaxPower);
        }

        public void Patch()
        {
            Log.LogInfo("Patching");

            Harmony harmony = new Harmony(pluginGuid);
            harmony.PatchAll();
        }
    }
}
