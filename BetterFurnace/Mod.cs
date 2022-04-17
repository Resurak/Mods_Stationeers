using Assets.Scripts.Objects.Pipes;
using BepInEx;
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

            // Creating harmony instance
            Harmony harmony = new Harmony(pluginGuid);

            // Getting original method from game and patched method
            var smelt_original = typeof(FurnaceBase).GetMethod(nameof(FurnaceBase.Smelt));
            var smelt_patched = typeof(Patches).GetMethod(nameof(Patches.Smelt_Patch));

            // Patching 
            Log.LogInfo("Patching FurnaceBase.Smelt");
            harmony.Patch(smelt_original, prefix: new HarmonyMethod(smelt_patched));

            Log.LogInfo("Loaded correclty");
        }
    }
}
