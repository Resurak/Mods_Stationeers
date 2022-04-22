using BepInEx.Configuration;
using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Config
{
    public class ConfigBool : Config
    {
        public bool Value { get; private set; }
        public ConfigEntry<bool> Entry { get; private set; }

        public ConfigBool(string plugin, string section, string key, string description, bool value) : base(plugin, section, key, description)
        {
            this.Value = value;
            this.Entry = base.Load(value);
        }
    }
}
