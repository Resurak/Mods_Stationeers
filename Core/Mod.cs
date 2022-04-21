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
        public const string pluginName = "Resurak Core";
        public const string pluginVersion = "0.1";

        // Mod logger
        public static ManualLogSource Log;

        public void Awake()
        {
            // Creating logger
            Log = base.Logger;
            Log.LogInfo("Loading mod");

            // Load Configs
            //Log.LogInfo("Loading config");
            //LoadConfig();

            // Creating harmony instance
            //Harmony harmony = new Harmony(pluginGuid);

            //Log.LogInfo("Patching");
            //harmony.PatchAll();

            Log.LogInfo("Loaded correclty");
        }
    }
}
