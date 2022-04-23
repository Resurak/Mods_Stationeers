using BepInEx;
using BepInEx.Configuration;
using Core.Shared;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.Config
{
    public class ConfigHandler
    {
        public string Plugin { get; private set; }
        public ConfigFile ConfigFile { get; private set; }

        public ConfigHandler(string plugin, ConfigFile configFile)
        {
            this.Plugin = plugin;
            this.ConfigFile = configFile;

            CoreLogger.Info(this.Plugin, "Loading configs");
        }

        public ConfigBool LoadBool(string section, string key, string description, bool value)
        {
            var config = new ConfigBool(section, key, description, value);
            config.Entry = ConfigFile.Bind(config.Section, config.Key, config.DefaultValue, config.Description);
            config.Loaded = true;

            Log(config.Section, config.Key, config.Value);
            return config;
        }

        public ConfigFloat LoadFloat(string section, string key, string description, float value)
        {
            var config = new ConfigFloat(section, key, description, value);
            config.Entry = ConfigFile.Bind(config.Section, config.Key, config.DefaultValue, config.Description);
            config.Loaded = true;

            Log(config.Section, config.Key, config.Value);
            return config;
        }

        public void Log(string section, string key, object value)
        {
            if (this.Plugin == Mod.pluginGuid)
            {
                CoreLogger.Info($"Loaded {section}.{key}. Value = {value}");
            }
            else if (Mod.Log_ShowConfigLoad.Value)
            {
                CoreLogger.Info(this.Plugin, $"Loaded {section}.{key}. Value = {value}");
            }
        }

        public bool MinMaxValid(float min, float max)
        {
            bool flag1 = min > max;
            bool flag2 = max < min;

            if (flag1)
            {
                CoreLogger.Error(this.Plugin, "Wrong configs. Min is bigger than Max");
            }

            if (flag2)
            {
                CoreLogger.Error(this.Plugin, "Wrong configs. Max is less than Min");
            }

            return !flag1 && !flag2;
        }
    }
}
