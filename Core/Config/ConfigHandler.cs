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

        private List<ConfigBool> ConfigList_Bool { get; set; }
        private List<ConfigFloat> ConfigList_Float { get; set; }

        private string ConfigPath => Path.Combine(Paths.ConfigPath, $"{Plugin}.cfg");

        public ConfigHandler(string plugin)
        {
            this.Plugin = plugin;
            this.ConfigFile = new ConfigFile(ConfigPath, true);

            this.ConfigList_Bool = new List<ConfigBool>();
            this.ConfigList_Float = new List<ConfigFloat>();

            CoreLogger.Info(this.Plugin, "Loading configs");
        }

        public ConfigBool AddBool(string section, string key, string description, bool value, bool load = true)
        {
            var entry = new ConfigBool(section, key, description, value);
            AddBool(entry, load);

            return entry;
        }

        public void AddBool(ConfigBool config, bool load = true)
        {
            this.ConfigList_Bool.Add(config);

            if (load &&  !config.Loaded)
            {
                Load(config);
            }
        }

        public ConfigFloat AddFloat(string section, string key, string description, float value, bool load = true)
        {
            var entry = new ConfigFloat(section, key, description, value);
            AddFloat(entry, load);

            return entry;
        }

        public void AddFloat(ConfigFloat config, bool load = true)
        {
            this.ConfigList_Float.Add(config);

            if (load && !config.Loaded)
            {
                Load(config);
            }
        }

        public void LoadAll()
        {
            foreach (var config in this.ConfigList_Bool.Where(x => !x.Loaded))
            {
                Load(config);
            }

            foreach (var config in this.ConfigList_Float.Where(x => !x.Loaded))
            {
                Load(config);
            }
        }

        public void Load(object config)
        {
            if (config is ConfigBool conf_bool)
            {
                conf_bool.Entry = ConfigFile.Bind(conf_bool.Section, conf_bool.Key, conf_bool.Value, conf_bool.Description);
                conf_bool.Loaded = true;

                Log(conf_bool.Section, conf_bool.Key, conf_bool.Value);
            }
            else if (config is ConfigFloat conf_float)
            {
                conf_float.Entry = ConfigFile.Bind(conf_float.Section, conf_float.Key, conf_float.Value, conf_float.Description);
                conf_float.Loaded = true;

                Log(conf_float.Section, conf_float.Key, conf_float.Value);
            }
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
