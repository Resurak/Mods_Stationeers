using BepInEx.Configuration;
using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Config
{
    public class ConfigBool : ConfigBase
    {
        public bool Value { get; private set; }
        public ConfigEntry<bool> Entry { get; set; }

        public ConfigBool(string section, string key, string description, bool value) : base(section, key, description)
        {
            this.Value = value;
        }
    }
}
