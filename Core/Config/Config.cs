using BepInEx;
using BepInEx.Configuration;
using Core.Shared;
using System.IO;

namespace Core.Config
{
    public class Config
    {
        public string Plugin { get; private set; }
        public ConfigFile ConfigFile { get; private set; }

        public string Key { get; set; }
        public string Section { get; set; }
        public string Description { get; set; }

        private string ConfigPath => Path.Combine(Paths.ConfigPath, $"{Plugin}.cfg");

        public Config(string plugin, string section, string key, string description)
        {
            this.Key = key;
            this.Plugin = plugin;
            this.Section = section;
            this.Description = description;

            this.ConfigFile = new ConfigFile(ConfigPath, true);
        }

        public ConfigEntry<T> Load<T>(T value)
        {
            ConfigEntry<T> entry = ConfigFile.Bind(this.Section, this.Key, value, this.Description);

            if (Mod.Log_ShowConfigLoad.Value)
            {
                CoreLogger.Info(this.Plugin, $"Loaded {this.Section}.{this.Key}. Value = {value}");
            }

            return entry;
        }
    }
}
