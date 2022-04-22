using BepInEx;
using BepInEx.Logging;
using Core.Shared;
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
        public const string pluginVersion = "0.6";

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

            CoreLogger.Info("Mod loaded correclty");
        }

        public void CreateLogger()
        {
            CoreLogger.Log = base.Logger;
            CoreLogger.Info("Loading mod");
        }

        public void LoadConfig()
        {
            CoreLogger.Warn("No configs to load");
        }

        public void Patch()
        {
            CoreLogger.Warn("No methods to patch");
        }
    }
}
