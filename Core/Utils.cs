using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core
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

        public static bool MinMaxValid(float min, float max) =>
            min >= max;

        public static void AssignMinMax(ref float value, float min, float max)
        {
            if (MinMaxValid(min, max))
            {
                if (value < min)
                {
                    value = min;
                }
                else if (value > max)
                {
                    value = max;
                }
            }
        }

        public static void AssignMinMax(ref float value, MinMax minMax) =>
            AssignMinMax(ref value, minMax.Min, minMax.Max);

        public static void AssignMinMax(ref float value, ConfigEntry<float> min, ConfigEntry<float> max) =>
            AssignMinMax(ref value, min.Value, max.Value);
    }
}
