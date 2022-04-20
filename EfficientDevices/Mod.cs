using Assets.Scripts.Objects.Pipes;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfficientDevices
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Mod : BaseUnityPlugin
    {
        // Plugin info
        public const string pluginGuid = "resurak.EfficientDevices";
        public const string pluginName = "Efficient Devices";
        public const string pluginVersion = "0.6";

        // Plugin config values
        public static ConfigEntry<float> AirConditioner_MaxPower;
        public static ConfigEntry<float> TurboVolumePump_MaxPower;

        // Mod logger
        internal static ManualLogSource Log;

        /// <summary>
        /// Called on mod load
        /// </summary>
        public void Awake()
        {
            // Creating logger
            Log = base.Logger;
            Log.LogInfo("Loading mod");

            // Load Configs
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
            AirConditioner_MaxPower = Config.Bind(
                "Power AirConditioner",
                "Max Power",
                10f,
                "Max power draw of the Air Conditioner");

            TurboVolumePump_MaxPower = Config.Bind(
                "Power TurboVolumePump(Gas)",
                "Max Power",
                200f,
                "Max power draw of the Turbo Volume Pump (Gas)");
        }
    }
}
