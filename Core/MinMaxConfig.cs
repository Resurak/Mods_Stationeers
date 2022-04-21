using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class MinMaxConfig
    {
        public MinMaxConfig(ConfigEntry<float> min, ConfigEntry<float> max) 
        {
            MinMax = MinMax.New(min, max);

            ConfigValid = false;
            ConfigChecked = false;
        }

        public MinMax MinMax { get; private set; }

        public bool ConfigValid { get; private set; }
        public bool ConfigChecked { get; private set; }

        public void CheckConfig(string caller)
        {
            if (!ConfigChecked)
            {
                ConfigValid = Utils.ConfigIsValid(caller, MinMax);
                ConfigChecked = true;
            }
        }
        
        public void Assign(ref float value)
        {
            if (ConfigValid)
            {
                Utils.AssignMinMax(ref value, MinMax);
            }
        }
    }
}
