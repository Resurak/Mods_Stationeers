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

namespace BetterFurnace
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Mod : BaseUnityPlugin
    {
        // Plugin info
        public const string pluginGuid = "resurak.BetterFurnace";
        public const string pluginName = "Better Furnace";
        public const string pluginVersion = "0.4";

        // Plugin config values
        public static ConfigEntry<int> Furnace_MinSetting;
        public static ConfigEntry<int> Furnace_MaxSetting;

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
            Furnace_MinSetting = Config.Bind(
                "Furnace",
                "Furnace_MinSetting",
                10,
                "Min Setting value of the furnace");

            Furnace_MaxSetting = Config.Bind(
                "Furnace",
                "Furnace_MaxSetting",
                200,
                "Max Setting value of the furnace");
        }
    }
}
