using Core.Config;

namespace Core.Shared
{
    public static class Utils
    {
        public static void AssignConfigValue(this ref float value, ConfigMinMax config)
        {
            if (config.Valid)
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
