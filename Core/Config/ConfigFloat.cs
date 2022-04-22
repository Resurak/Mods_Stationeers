using BepInEx.Configuration;
using Core.Shared;

namespace Core.Config
{
    public class ConfigFloat : ConfigBase
    {
        public float Value { get; private set; }
        public ConfigEntry<float> Entry { get; set; }

        public ConfigFloat(string section, string key, string description, float value) : base(section, key, description)
        {
            this.Value = value;
        }
    }
}
