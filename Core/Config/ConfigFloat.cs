using BepInEx.Configuration;
using Core.Shared;

namespace Core.Config
{
    public class ConfigFloat : Config
    {
        public float Value { get; private set; }
        public ConfigEntry<float> Entry { get; private set; }

        public ConfigFloat(string plugin, string section, string key, string description, float value) : base(plugin, section, key, description)
        {
            this.Value = value;
            this.Entry = base.Load(value);
        }
    }
}
