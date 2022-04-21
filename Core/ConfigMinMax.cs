using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ConfigMinMax
    {
        public ConfigMinMax(ConfigEntry<float> min, ConfigEntry<float> max) 
        {
            Values = MinMax.New(min, max);

            ConfigValid = false;
            ConfigChecked = false;
        }

        public MinMax Values { get; private set; }

        public bool ConfigValid { get; private set; }
        public bool ConfigChecked { get; private set; }

        public void CheckConfig(string caller)
        {
            try
            {
                if (!ConfigChecked)
                {
                    ConfigValid = Utils.ConfigIsValid(caller, Values);
                    ConfigChecked = true;

                    Mod.Log.LogInfo($"{caller}: {nameof(ConfigValid)} = {ConfigValid}");
                }
            }
            catch (Exception ex)
            {
                Mod.Log.LogError("Exception thrown while checking config. Exception: " + ex.ToString());
            }
        }
    }
}
