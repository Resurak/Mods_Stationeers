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
        public const string pluginVersion = "0.8";

        // Plugin config values
        public static ConfigEntry<float> Cable_MaxVoltage;

        public static ConfigEntry<float> AirConditioner_MinPower;
        public static ConfigEntry<float> AirConditioner_MaxPower;

        public static ConfigEntry<float> TurboVolumePump_MinPower;
        public static ConfigEntry<float> TurboVolumePump_MaxPower;

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

            Cable_MaxVoltage = Config.Bind(
                "Cable",
                "Max voltage",
                5000f,
                "Max voltage that a cable can absorb");
            Log.LogInfo($"{nameof(Cable_MaxVoltage)} = {Cable_MaxVoltage.Value}");

            AirConditioner_MinPower = Config.Bind(
                "Air Conditioner",
                "Min Power",
                100f,
                "Min power draw of the Air Conditioner");
            Log.LogInfo($"{nameof(AirConditioner_MinPower)} = {AirConditioner_MinPower.Value}");

            AirConditioner_MaxPower = Config.Bind(
                "Air Conditioner",
                "Max Power",
                1500f,
                "Max power draw of the Air Conditioner");
            Log.LogInfo($"{nameof(AirConditioner_MaxPower)} = {AirConditioner_MaxPower.Value}");

            TurboVolumePump_MinPower = Config.Bind(
                "Turbo Volume Pump (Gas)",
                "Min Power",
                50f,
                "Min power draw of the Turbo Volume Pump (Gas)");
            Log.LogInfo($"{nameof(TurboVolumePump_MinPower)} = {TurboVolumePump_MinPower.Value}");

            TurboVolumePump_MaxPower = Config.Bind(
                "Turbo Volume Pump (Gas)",
                "Max Power",
                200f,
                "Max power draw of the Turbo Volume Pump (Gas)");
            Log.LogInfo($"{nameof(TurboVolumePump_MaxPower)} = {TurboVolumePump_MaxPower.Value}");
        }

        public void Patch()
        {
            Log.LogInfo("Patching");

            Harmony harmony = new Harmony(pluginGuid);
            harmony.PatchAll();
        }
    }
}
