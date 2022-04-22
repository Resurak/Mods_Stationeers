using BepInEx.Configuration;
using Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared
{
    public class Utils
    {
        public static bool ConfigIsValid(string caller, float min, float max)
        {
            bool flag1 = min > max;
            bool flag2 = max < min;

            if (flag1)
            {
                CoreLogger.Error(caller, "Wrong configs. Min is bigger than Max");
            }

            if (flag2)
            {
                CoreLogger.Error(caller, "Wrong configs. Max is less than Min");
            }

            return !flag1 && !flag2;
        }

        public static void AssignConfigValue(ref float value, ConfigMinMax config)
        {
            if (config.ConfigValid)
            {
                if (value < config.Min)
                {
                    value = config.Min;
                }
                else if (value > config.Max)
                {
                    value = config.Max;
                }
            }
        }
    }
}
