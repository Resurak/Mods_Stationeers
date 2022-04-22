using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Config
{
    public class ConfigMinMax 
    {
        public ConfigMinMax(ConfigHandler handler, ConfigFloat min, ConfigFloat max)
        {
            this.configMin = min;
            this.configMax = max;

            this.Valid = handler.MinMaxValid(Min, Max);
        }

        public float Min => configMin.Value;
        public float Max => configMax.Value;
        public bool Valid { get; private set; }

        private ConfigFloat configMin;
        private ConfigFloat configMax;
    }
}
