using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public struct MinMax
    {
        public MinMax(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float Min { get; set; }
        public float Max { get; set; }

        public static MinMax New(ConfigEntry<float> min, ConfigEntry<float> max) =>
            new MinMax(min.Value, max.Value);
    }
}
