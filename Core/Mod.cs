using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Mod : BaseUnityPlugin
    {
        // Plugin info
        public const string pluginGuid = "resurak.Core";
        public const string pluginName = "Resurak Mod Core";
        public const string pluginVersion = "0.4";

        // Mod logger
        public static ManualLogSource Log;

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
            Log.LogInfo("No config to load");
        }

        public void Patch()
        {
            Log.LogInfo("Patching");

            Harmony harmony = new Harmony(pluginGuid);
            harmony.PatchAll();
        }
    }
}
