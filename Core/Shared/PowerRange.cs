using BepInEx.Configuration;
using Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared
{
    public class PowerRange
    {
        public float Min { get; private set; } 
        public float Max { get; private set; }
        public float MinDefault { get; private set; }

        public bool Valid { get; private set; }

        public PowerRange(ConfigFloat max, float minDefault = 0f)
        {
            this.Min = float.MinValue;
            this.Max = max.Value;
            this.MinDefault = minDefault;

            if (this.Max < MinDefault)
            {
                CoreLogger.Warn($"{max.Section}.{max.Key} value is less than {this.MinDefault}");
                this.Valid = false;
            }
            else
            {
                this.Valid = true;
            }
        }

        public PowerRange(ConfigFloat min, ConfigFloat max, float minDefault = 0f)
        {
            this.Min = min.Value;
            this.Max = max.Value;
            this.MinDefault = minDefault;

            bool flag1 = this.Min >= MinDefault;
            if (!flag1)
            {
                CoreLogger.Warn($"{min.Section}.{min.Key} value is less than {this.MinDefault}");
            }

            bool flag2 = this.Min < this.Max;
            if (!flag2)
            {
                CoreLogger.Warn($"{min.Section}.{min.Key} value is greater or equal to {max.Section}.{max.Key}");
            }

            this.Valid = flag1 && flag2;
        }
    }
}
