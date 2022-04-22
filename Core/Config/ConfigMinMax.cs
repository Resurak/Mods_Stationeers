using Core.Shared;

namespace Core.Config
{
    public class ConfigMinMax
    {
        public ConfigMinMax(string key, string plugin, ConfigFloat min, ConfigFloat max)
        {
            this.Key = key;
            this.Plugin = plugin;

            this.ConfigMin = min;
            this.ConfigMax = max;

            this.ConfigValid = false;
            this.ConfigChecked = false;

            CheckConfig();
        }

        public string Key { get; private set; }
        public string Plugin { get; private set; }

        public bool ConfigValid { get; private set; }
        public bool ConfigChecked { get; private set; }

        public float Min => ConfigMin.Value;
        public float Max => ConfigMax.Value;

        private ConfigFloat ConfigMin { get; set; }
        private ConfigFloat ConfigMax { get; set; }

        public void CheckConfig()
        {
            if (!ConfigChecked)
            {
                ConfigValid = Utils.ConfigIsValid(this.Plugin, this.Min, this.Max);
                ConfigChecked = true;

                if (Mod.Log_ShowConfigValid.Value)
                {
                    CoreLogger.Info($"{this.Plugin}.{this.Key}", $"{nameof(ConfigValid)} = {ConfigValid}");
                }
            }
        }
    }
}
