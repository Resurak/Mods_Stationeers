using Assets.Scripts.Objects.Pipes;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Core;
using Core.Shared;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public const string pluginVersion = "0.9";

        // Plugin config values
        public static ConfigMinMax Furnace_Config;
        public static ConfigEntry<float> Furnace_MinSetting;
        public static ConfigEntry<float> Furnace_MaxSetting;

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

            Furnace_MinSetting = Config.Bind(
                "Furnace",
                "Min Setting",
                1f,
                "Min Setting value of the furnace");
            Log.LogInfo($"{nameof(Furnace_MinSetting)} = {Furnace_MinSetting.Value}");

            Furnace_MaxSetting = Config.Bind(
                "Furnace",
                "Max Setting",
                100f,
                "Max Setting value of the furnace");
            Log.LogInfo($"{nameof(Furnace_MaxSetting)} = {Furnace_MaxSetting.Value}");

            Furnace_Config = new ConfigMinMax(Furnace_MinSetting, Furnace_MaxSetting);
            Furnace_Config.CheckConfig(nameof(Furnace_Config));
        }

        public void Patch()
        {
            Log.LogInfo("Patching");

            Harmony harmony = new Harmony(pluginGuid);
            harmony.PatchAll();
        }
    }
}
