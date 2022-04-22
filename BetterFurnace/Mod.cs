using Assets.Scripts.Objects.Pipes;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Core;
using Core.Config;
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
        public static ConfigFloat Furnace_MinSetting;
        public static ConfigFloat Furnace_MaxSetting;

        public static ConfigFloat Furnace_Min;

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

            Furnace_MinSetting = new ConfigFloat(pluginGuid, "Furnace", "MinSetting", "Minimum Setting value for the furnace", 1f);
            Furnace_MaxSetting = new ConfigFloat(pluginGuid, "Furnace", "MaxSetting", "Maximum Setting value for the furnace", 100f);

            Furnace_Config = new ConfigMinMax(nameof(Furnace_Config), pluginGuid, Furnace_MinSetting, Furnace_MaxSetting);
        }

        public void Patch()
        {
            Log.LogInfo("Patching");

            Harmony harmony = new Harmony(pluginGuid);
            harmony.PatchAll();
        }
    }
}
