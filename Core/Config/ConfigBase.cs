using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Config
{
    public class ConfigBase
    {
        public ConfigBase(string key, string section, string description)
        {
            this.Key = key;
            this.Section = section;
            this.Description = description;
        }

        public string Key { get; set; }
        public string Section { get; set; }
        public string Description { get; set; }

        public bool Loaded { get; set; }
    }
}
