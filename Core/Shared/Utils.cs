using BepInEx.Configuration;
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
        public static bool ConfigIsValid(string caller, MinMax minMax) =>
            ConfigIsValid(caller, minMax.Min, minMax.Max);

        public static bool ConfigIsValid(string caller, float min, float max)
        {
            bool flag1 = min > max;
            bool flag2 = max < min;

            if (flag1)
            {
                Mod.Log.LogError($"{caller}: Wrong configs. Min is bigger than Max");
            }

            if (flag2)
            {
                Mod.Log.LogError($"{caller}: Wrong configs. Max is less than Min");
            }

            return !flag1 && !flag2;
        }

        public static void MinMax(ref float value, ConfigMinMax config)
        {
            if (config.ConfigValid)
            {
                if (value < config.Values.Min)
                {
                    value = config.Values.Min;
                }
                else if (value > config.Values.Max)
                {
                    value = config.Values.Max;
                }
            }
        }
    }
}
