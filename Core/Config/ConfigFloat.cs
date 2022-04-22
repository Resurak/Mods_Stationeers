using BepInEx.Configuration;
using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Config
{
    public class ConfigFloat : Config
    {
        public float Value { get; private set; }
        public ConfigEntry<float> Entry { get; private set; }

        public ConfigFloat(string plugin, string section, string key, string description, float value) : base(plugin, section, key, description)
        {
            this.Value = value;
            Load();
        }

        public void Load()
        {
            this.Entry = ConfigFile.Bind(this.Section, this.Key, this.Value, this.Description);
            CoreLogger.Info(this.Plugin, $"Loaded {this.Section}.{this.Key}. Value = {this.Value}");
        }
    }
}
